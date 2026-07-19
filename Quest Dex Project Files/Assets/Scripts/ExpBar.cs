using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Slider slider;

    private void Update()
    {
        float exp = FindAnyObjectByType<MenuManager>().experienceBar;
        float maxexp = FindAnyObjectByType<MenuManager>().maxExperienceBar;

        slider.value = exp / maxexp;
    }
}
