using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public string foodType;
    [SerializeField] int cost;
    [SerializeField] TMP_Text costText;
    [SerializeField] MenuManager menu;

    private void Start()
    {
        if (costText != null) { costText.text = cost.ToString(); }
    }

    public void BuyItem()
    {
        if (menu == null) { return; }

        menu.buyFood(GetComponent<Food>().cost, foodType, this);
    }

}
