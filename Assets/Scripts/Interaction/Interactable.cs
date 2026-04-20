using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Cualquier obj que sea interactuable tendra este script
public class Interactable : MonoBehaviour
{
    [SerializeField] private LoadScene load;
    [SerializeField] private Preload preload;
    private GameObject script_load;
    //public UnityEvent onInteract;

    private void Start()
    {
        if (script_load == null)
        {
            script_load = GameObject.Find("--SceneManagement--");
            load = script_load.GetComponent<LoadScene>();
            preload = script_load.GetComponent<Preload>();
        }
    }
    public void Interact()
    {
       // onInteract?.Invoke();
    }
    public void DetectObj(GameObject a)
    {
        switch (a.tag)
        {
            case "Enemy":

                Debug.Log("This is a Enemy");
                Debug.Log(a.name);
                preload.CombatOpponent( a.name );
                load.Combat(/*a*/);
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
