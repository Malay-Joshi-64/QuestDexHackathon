using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SelectorManager : MonoBehaviour
{
    [SerializeField] string[] pokemonNames;
    [SerializeField] TMP_Text pokemonNameText;
    [SerializeField] TMP_Text pokemonNameTextShadow;
    [SerializeField] Sprite[] characterSprite;
    [SerializeField] Image spriteHolder;
    [SerializeField] int characterSpriteInt = 0;
    [SerializeField] Button continueButton;
    [SerializeField] TMP_Text confirmbtnText;

    [SerializeField] AudioSource source;
    [SerializeField] AudioClip pokemonCry;

    private void Awake()
    {
        spriteHolder.sprite = characterSprite[characterSpriteInt];
        pokemonNameText.text = pokemonNames[characterSpriteInt];
        pokemonNameTextShadow.text = pokemonNames[characterSpriteInt];
    }

    private void Start()
    {
        continueButton.interactable = PlayerPrefs.HasKey("PokemonInt");

        if (PlayerPrefs.HasKey("PokemonInt"))
        {
            confirmbtnText.text = "Change to\n" + pokemonNames[characterSpriteInt];
        }

        else
        {
            confirmbtnText.text = "Choose " + pokemonNames[characterSpriteInt];
        }
    }

    public void leftArrow()
    {
        if (characterSpriteInt <= 0) { characterSpriteInt = characterSprite.Length-1; }

        else { characterSpriteInt--; }

        spriteHolder.sprite = characterSprite[characterSpriteInt];
        pokemonNameText.text = pokemonNames[characterSpriteInt];
        pokemonNameTextShadow.text = pokemonNames[characterSpriteInt];

        if (PlayerPrefs.HasKey("PokemonInt"))
        {
            confirmbtnText.text = "Change to\n" + pokemonNames[characterSpriteInt];
        }

        else
        {
            confirmbtnText.text = "Choose " + pokemonNames[characterSpriteInt];
        }
    }

    public void rightArrow()
    {

        if (characterSpriteInt > characterSprite.Length-2) { characterSpriteInt = 0; }

        else { characterSpriteInt++; }

        spriteHolder.sprite = characterSprite[characterSpriteInt];
        pokemonNameText.text = pokemonNames[characterSpriteInt];
        pokemonNameTextShadow.text = pokemonNames[characterSpriteInt];

        if (PlayerPrefs.HasKey("PokemonInt"))
        {
            confirmbtnText.text = "Change to\n" + pokemonNames[characterSpriteInt];
        }

        else
        {
            confirmbtnText.text = "Choose " + pokemonNames[characterSpriteInt];
        }
    }

    public void confirmPokemon()
    {
        StartCoroutine("changeScene");
    }

    IEnumerator changeScene()
    {
        PlayerPrefs.SetInt("PokemonInt", characterSpriteInt);
        PlayerPrefs.SetInt("pokemonStage", 0);
        PlayerPrefs.SetFloat("experience", 0);
        PlayerPrefs.Save();

        source.PlayOneShot(pokemonCry);
        yield return new WaitForSeconds(pokemonCry.length);

        StartCoroutine("LoadAsyncScene");
    }

    public void continueProgress()
    {
        StartCoroutine("LoadAsyncScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation o = SceneManager.LoadSceneAsync(1);

        while (!o.isDone)
        {
            yield return null;
        }
    }
}
