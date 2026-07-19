using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using JetBrains.Annotations;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject newTaskPanel;
    [SerializeField] GameObject goalListPanel;
    [SerializeField] GameObject pokemonPanel;
    [SerializeField] GameObject pokeFoodPanel;
    [SerializeField] TMP_InputField taskNameInput;
    [SerializeField] TMP_Dropdown taskPriorityDropdown;
    [SerializeField] TMP_Text coinText;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] float yOffsetMultiplier;
    [SerializeField] Transform prefabParent;
    [SerializeField] List<Task> activeTasks = new List<Task>();
    private int coins = 0;
    public int food1 = 0;
    public int food2 = 0;
    public int food3 = 0;
    public int food4 = 0;
    public float experienceBar = 0;
    public float maxExperienceBar = 60;
    public int pokemonInt = 0;
    [SerializeField] Button evolutionButton;
    [SerializeField] TMP_Text evolutionButtonText;
    public string pokemonName = "";
    [SerializeField] TMP_Text pokemonNameHolder;
    public int pokemonStage = 0;

    [SerializeField] GameObject alertTextPanel;
    [SerializeField] TMP_Text alertTextHolder;

    [SerializeField] Consumefood f1;
    [SerializeField] Consumefood f2;
    [SerializeField] Consumefood f3;
    [SerializeField] Consumefood f4;

    [SerializeField] Image spriteHolder;

    [SerializeField] ParticleSystem heartParticles;
    [SerializeField] ParticleSystem evolveParticles;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip coinScatter;
    [SerializeField] AudioClip evolutionSoundCLip;
    [SerializeField] AudioClip pokemonCry;

    [SerializeField] ScrollRect goalScrollRect;
    [SerializeField] RectTransform goalContentRect;

    [System.Serializable]
    public class Evolution
    {
        public List<Sprite> evolutionSprites;
        public List<String> evolutionNames;
    }

    public List<Evolution> evolutions;

    private void Awake()
    {

        evolutionButton.interactable = experienceBar >= maxExperienceBar;
        newTaskPanel.SetActive(false);
        goalListPanel.SetActive(false);
        pokemonPanel.SetActive(false);
        pokeFoodPanel.SetActive(false);
        alertTextPanel.SetActive(false);

        string[] namesList;
        string[] priortiesList;

        namesList = PlayerPrefs.GetString("names").Split(",");
        priortiesList = PlayerPrefs.GetString("priorities").Split(",");

        evolutionButtonText.text = "Train";
        if (PlayerPrefs.HasKey("coins")) { coins = PlayerPrefs.GetInt("coins"); }
        if (PlayerPrefs.HasKey("food1")) { food1 = PlayerPrefs.GetInt("food1"); }
        if (PlayerPrefs.HasKey("food2")) { food2 = PlayerPrefs.GetInt("food2"); }
        if (PlayerPrefs.HasKey("food3")) { food3 = PlayerPrefs.GetInt("food3"); }
        if (PlayerPrefs.HasKey("food4")) { food4 = PlayerPrefs.GetInt("food4"); }
        if (PlayerPrefs.HasKey("PokemonInt")) { pokemonInt = PlayerPrefs.GetInt("PokemonInt"); }
        if (PlayerPrefs.HasKey("pokemonStage")) 
        {   pokemonStage = PlayerPrefs.GetInt("pokemonStage");
            pokemonName = evolutions[pokemonInt].evolutionNames[pokemonStage].ToString();
        }
        if (PlayerPrefs.HasKey("experience")) { experienceBar = PlayerPrefs.GetFloat("experience"); }
        coinText.text = coins.ToString();

        if (pokemonName == "0") { pokemonName = evolutions[pokemonInt].evolutionNames[pokemonStage].ToString(); }
        pokemonNameHolder.text = pokemonName;

        if (pokemonName == evolutions[pokemonInt].evolutionNames[pokemonStage][evolutions[pokemonInt].evolutionNames.Count -1].ToString())
        {
            evolutionButtonText.text = "Maxed";
            evolutionButton.interactable = false;
        }

        spriteHolder.sprite = evolutions[pokemonInt].evolutionSprites[pokemonStage];

        for (int j = 0; j < namesList.Length; j++)
        {
            if (string.IsNullOrEmpty(namesList[j]) || string.IsNullOrEmpty(priortiesList[j]))
            {
                return;
            }

            GameObject task = Instantiate(itemPrefab, prefabParent.transform.position, Quaternion.identity);
            task.transform.parent = prefabParent.transform;
            task.transform.localScale = new Vector3(0.34593001f, 0.478594154f, 0.34593001f);
            int p = 0;

            switch (priortiesList[j])
            {
                case "High":
                    p = 1;
                    break;

                case "Medium":
                    p = 2;
                    break;

                case "Low":
                    p = 3;
                    break;

            }

            task.GetComponent<Task>()._Setup(namesList[j], p);

            activeTasks.Add(task.GetComponent<Task>());
        }
    }

    public void AddGoalDropdown()
    {
        newTaskPanel.SetActive(!newTaskPanel.activeSelf);
    }

    public void CheckGoalDropdown()
    {
        goalListPanel.SetActive(!goalListPanel.activeSelf);
    }

    public void openPokemonMenu()
    {
        pokemonPanel.SetActive(!pokemonPanel.activeSelf);
    }

    public void openPokeShop()
    {
        pokeFoodPanel.SetActive(!pokeFoodPanel.activeSelf);
    }

    public void changePokemon()
    {

        //save
        string taskNameString = "";
        string taskPriorityString = "";

        foreach (Task t in activeTasks)
        {
            taskNameString += t.taskNameHeader.text.Split(" (")[0] + ",";
            taskPriorityString += t.priorText.Replace(" ", "") + ",";
        }

        PlayerPrefs.SetString("names", taskNameString);
        PlayerPrefs.SetString("priorities", taskPriorityString);
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("food1", food1);
        PlayerPrefs.SetInt("food2", food2);
        PlayerPrefs.SetInt("food3", food3);
        PlayerPrefs.SetInt("food4", food4);
        PlayerPrefs.SetString("pokemonName", pokemonName);
        PlayerPrefs.SetFloat("experience", experienceBar);
        PlayerPrefs.SetInt("pokemonStage", pokemonStage);
        PlayerPrefs.SetInt("PokemonInt", pokemonInt);
        PlayerPrefs.Save();

        activeTasks.Clear();

        StartCoroutine("LoadAsyncScene");
    }

    public void buyFood(int cost, string foodType, Food f)
    {

        if (coins < cost)
        {
            AlertBox("Not Sufficient PokeCreds\nComplete More Tasks");
        }

        if (coins >= cost)
        {
            coins -= cost;
            coinText.text = coins.ToString();
            source.PlayOneShot(coinScatter);

            switch (foodType)
            {
                case "food_1":
                    food1++;
                    f1.UpdateConsumables();
                    break;

                case "food_2":
                    food2++;
                    f2.UpdateConsumables();
                    break;

                case "food_3":
                    food3++;
                    f3.UpdateConsumables();
                    break;

                case "food_4":
                    food4++;
                    f4.UpdateConsumables();
                    break;
            }
        }
    }

    public void consumeFood(int x, Consumefood cf, string foodType)
    {
        experienceBar += x;

        switch (foodType)
        {
            case "food_1":
                food1--;
                break;

            case "food_2":
                food2--;
                break;

            case "food_3":
                food3--;
                break;

            case "food_4":
                food4--;
                break;
        }

        cf.UpdateConsumables();

        if (pokemonStage >= 2) { return; }

        evolutionButton.interactable = experienceBar >= maxExperienceBar;

        if (experienceBar >= maxExperienceBar) { evolutionButtonText.text = "Evolve"; }

        heartParticles.Play();
        
        if (!source.isPlaying)
        {
            source.PlayOneShot(pokemonCry);
        }
    }

    public void StartEvolution()
    {
        if (pokemonStage >= 2) { return; }

        StartCoroutine("EvolvePokemon");
    }

    IEnumerator EvolvePokemon()
    {

        pokemonStage++;
        evolveParticles.Play();
        source.PlayOneShot(evolutionSoundCLip);

        yield return new WaitForSeconds(1f);

        pokemonName = evolutions[pokemonInt].evolutionNames[pokemonStage].ToString();
        pokemonNameHolder.text = pokemonName;
        spriteHolder.sprite = evolutions[pokemonInt].evolutionSprites[pokemonStage];
        experienceBar -= maxExperienceBar;
        evolutionButton.interactable = false;

        if (pokemonStage == 2)
        {
            evolutionButtonText.text = "Maxed";
            evolutionButton.interactable = false;
        }

        if (pokemonStage < 2)
        {
            evolutionButtonText.text = "Train";
        }
    }

    public void AlertBox(string s)
    {
        alertTextHolder.text = s;
        alertTextPanel.SetActive(true);
    }

    public void CloseAlertBox() { alertTextPanel.SetActive(false); }

    public void AddNewTask()
    {
        if (string.IsNullOrEmpty(taskNameInput.text) || string.IsNullOrWhiteSpace(taskNameInput.text))
        {
            AlertBox("Fill Goal Name");
            return;
        }

        if (taskPriorityDropdown.value == 0)
        {
            AlertBox("Priority Not Specified");
            return;
        }


        GameObject task = Instantiate(itemPrefab, prefabParent.transform.position, Quaternion.identity);
        task.transform.parent = prefabParent.transform;
        task.GetComponent<Task>()._Setup(taskNameInput.text, taskPriorityDropdown.value);
        task.transform.localScale = new Vector3(0.34593001f, 0.478594154f, 0.34593001f);

        activeTasks.Add(task.GetComponent<Task>());

        AlertBox("New Goal Added");
        newTaskPanel.SetActive(false);
    }

    public void RemoveTask(GameObject g)
    {
        activeTasks.Remove(g.GetComponent<Task>());
        switch (g.GetComponent<Task>().priorInt)
        {
            case 1:
                coins += 15;
                break;
            case 2:
                coins += 10;
                break;
            case 3:
                coins += 5;
                break;
        }
        coinText.text = coins.ToString();
        source.PlayOneShot(coinScatter);

        float savedScrollPos = goalScrollRect.verticalNormalizedPosition;
        Destroy(g);

        // force layout to recalculate NOW instead of waiting
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(goalContentRect);
        goalScrollRect.verticalNormalizedPosition = Mathf.Clamp01(savedScrollPos);
    }

    private void OnApplicationQuit()
    {
        string taskNameString = "";
        string taskPriorityString = "";

        foreach (Task t in activeTasks)
        {
            taskNameString += t.taskNameHeader.text.Split(" (")[0] + ",";
            taskPriorityString += t.priorText.Replace(" ", "") + ",";
        }

        PlayerPrefs.SetString("names", taskNameString);
        PlayerPrefs.SetString("priorities", taskPriorityString);
        PlayerPrefs.SetInt("coins", coins);
        PlayerPrefs.SetInt("food1", food1);
        PlayerPrefs.SetInt("food2", food2);
        PlayerPrefs.SetInt("food3", food3);
        PlayerPrefs.SetInt("food4", food4);
        PlayerPrefs.SetString("pokemonName", pokemonName);
        PlayerPrefs.SetFloat("experience", experienceBar);
        PlayerPrefs.SetInt("pokemonStage", pokemonStage);
        PlayerPrefs.SetInt("PokemonInt", pokemonInt);
        PlayerPrefs.Save();

        activeTasks.Clear();
    }
    
    IEnumerator LoadAsyncScene()
    {
        AsyncOperation o = SceneManager.LoadSceneAsync(0);

        while (!o.isDone)
        {
            yield return null;
        }
    }

}

