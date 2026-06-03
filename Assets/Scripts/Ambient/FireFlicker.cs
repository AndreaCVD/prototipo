using UnityEngine;

public class FireFlicker : MonoBehaviour
{
    [SerializeField] private Light fireLight;
    [SerializeField] private float minIntensity = 2f;
    [SerializeField] private float maxIntensity = 5f;
    [SerializeField] private float flickerSpeed = 8f;

    void Update()
    {
        fireLight.intensity = Mathf.Lerp(
            minIntensity,
            maxIntensity,
            Mathf.PerlinNoise(Time.time * flickerSpeed, 0f)
        );
    }
}