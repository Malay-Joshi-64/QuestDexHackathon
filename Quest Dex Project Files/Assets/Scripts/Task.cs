using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    public TMP_Text taskNameHeader;
    public int priorInt = 0;
    public string priorText = "";

    public void _Setup(string taskName, int P)
    {
        string priority = "";
        priorInt = P;

        switch (P)
        {
            case 1:
                priority = "High";
                break;

            case 2:
                priority = "Medium";
                break;

            case 3:
                priority = "Low";
                break;

        }
        priorText = priority;

        taskNameHeader.text = taskName + " ("+ priority + ")";
    }

    public void _CompleteTask()
    {
        FindAnyObjectByType<MenuManager>().RemoveTask(gameObject);
    }

}
