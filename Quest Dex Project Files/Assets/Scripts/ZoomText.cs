using UnityEngine;

public class ZoomText : MonoBehaviour
{
    public float pulseSpeed = 1.5f;
    public float minScale = 0.95f;
    public float maxScale = 1.05f;

    Vector3 baseScale;

    void Start()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f;
        float scale = Mathf.Lerp(minScale, maxScale, t);
        transform.localScale = baseScale * scale;
    }
}
