using UnityEngine;

public class CajaArrastrable : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody rb;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
