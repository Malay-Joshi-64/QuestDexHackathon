using UnityEngine;

public class SpriteWobble : MonoBehaviour
{
    public float wobbleSpeed = 1.5f;
    public float wobbleAngle = 5f; // max rotation in degrees, either direction

    void Update()
    {
        float angle = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAngle;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
