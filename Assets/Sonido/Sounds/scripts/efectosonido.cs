using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class efectosonido : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControladorSonido.Instance.EjecutarSonido(sound);
            //Destroy(gameObject);
        }
    }
    
}