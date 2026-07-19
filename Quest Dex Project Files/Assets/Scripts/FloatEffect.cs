using Unity.InferenceEngine.Tokenization.Padding;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    public float floatSpeedY = 2f;
    public float floatHeightY = 15f;

    public float floatSpeedX = 2f;
    public float floatWidthX = 0f; // set to 0 to disable X movement

    RectTransform rectTransform;
    Vector2 startPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * floatSpeedY) * floatHeightY;
        float offsetX = Mathf.Sin(Time.time * floatSpeedX) * floatWidthX;
        rectTransform.anchoredPosition = startPos + new Vector2(offsetX, offsetY);
    }

}