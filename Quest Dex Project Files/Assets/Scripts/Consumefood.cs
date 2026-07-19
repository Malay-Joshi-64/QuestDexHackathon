using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Consumefood : MonoBehaviour
{
    public string foodType;
    public int count;
    [SerializeField] int xp;
    [SerializeField] TMP_Text countText;

    private void Start()
    {
        switch (foodType)
        {
            case "food_1":
                count = FindAnyObjectByType<MenuManager>().food1;
                break;

            case "food_2":
                count = FindAnyObjectByType<MenuManager>().food2;
                break;

            case "food_3":
                count = FindAnyObjectByType<MenuManager>().food3;
                break;

            case "food_4":
                count = FindAnyObjectByType<MenuManager>().food4;
                break;
        }

        if (countText != null) { countText.text = count.ToString(); }
    }

    public void UpdateConsumables()
    {
        switch (foodType)
        {
            case "food_1":
                count = FindAnyObjectByType<MenuManager>().food1;
                break;

            case "food_2":
                count = FindAnyObjectByType<MenuManager>().food2;
                break;

            case "food_3":
                count = FindAnyObjectByType<MenuManager>().food3;
                break;

            case "food_4":
                count = FindAnyObjectByType<MenuManager>().food4;
                break;
        }

        if (countText != null) { countText.text = count.ToString(); }
    }

    public void usefood()
    {
        
        if (count <= 0)
        {
            FindAnyObjectByType<MenuManager>().AlertBox("No Quantity");
            return; 
        }

        FindAnyObjectByType<MenuManager>().consumeFood(xp, this, foodType);
    }
}
