using UnityEngine;

public class SonidoPasos : MonoBehaviour
{
    public AudioSource pie;

    public float velocidadMinima = 0.1f;
    private Rigidbody rb;

    private float cooldown = 0.4f;
    private float tiempo;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        tiempo += Time.deltaTime;

        if (rb.linearVelocity.magnitude > velocidadMinima && tiempo > cooldown)
        {
            pie.Play();
            tiempo = 0f;
        }
    }
}