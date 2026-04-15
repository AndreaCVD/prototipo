using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Cualquier obj que sea interactuable tendra este script
public class Interactable : MonoBehaviour
{
    public UnityEvent onInteract;
    public void Interact()
    {
        onInteract?.Invoke();
    }
    public void DetectObj(GameObject a)
    {
        switch (a.tag)
        {
            case "Enemy":
                Debug.Log("This is a Enemy");
                break;
            case "Puzzle":
                Debug.Log("This is a Puzzle");
                break;
            default:
                Debug.Log("No hay nada");
                break;
        }


    }
}
