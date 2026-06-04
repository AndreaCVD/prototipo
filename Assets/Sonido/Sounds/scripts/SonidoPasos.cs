using UnityEngine;

public class SonidoPasos : MonoBehaviour
{
    public AudioSource audioSource;

    public float cooldown = 0.4f;

    private float tiempo;

    void Update()
    {
        tiempo += Time.deltaTime;

        Vector3 movimiento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (movimiento.magnitude > 0.1f && tiempo > cooldown)
        {
            audioSource.PlayOneShot(audioSource.clip);
            tiempo = 0f;
        }
    }
}