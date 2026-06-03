using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class efectosonido : MonoBehaviour
{
    [SerializeField] private AudioClip dragbox;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ControladorSonido.Instance.EjecutarSonido(dragbox);
            //Destroy(gameObject);
        }
    }
    
}