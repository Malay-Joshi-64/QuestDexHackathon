using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pokeball : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image pokeballSpriteHolder;
    [SerializeField] Sprite closedImage;
    [SerializeField] Sprite openImage;
    [SerializeField] TMP_Text pokeballText;

    [SerializeField] float exitDelay = 0.15f;
    Coroutine exitRoutine;

    private void Start()
    {
        pokeballSpriteHolder.sprite = closedImage;
        pokeballText.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (exitRoutine != null)
        {
            StopCoroutine(exitRoutine);
            exitRoutine = null;
        }
        pokeballSpriteHolder.sprite = openImage;
        pokeballText.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        exitRoutine = StartCoroutine(DelayedExit());
    }

    IEnumerator DelayedExit()
    {
        yield return new WaitForSeconds(exitDelay);
        pokeballSpriteHolder.sprite = closedImage;
        pokeballText.enabled = false;
        exitRoutine = null;
    }


}
