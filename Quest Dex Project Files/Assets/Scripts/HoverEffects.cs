using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float hoverScale = 1.1f;
    public float speed = 8f;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip hoverSound;
    [SerializeField] AudioClip clickSound;
    [SerializeField] bool clickSounds = false;

    Vector3 originalScale;
    Vector3 targetScale;

    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
        source.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (source.isPlaying) { return; }
        if (clickSounds) { source.PlayOneShot(clickSound); }
    }
}
