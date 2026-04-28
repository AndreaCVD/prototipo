using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//Cualquier obj que sea interactuable tendra este script
public class Interactable : MonoBehaviour
{
    [Header("EL DIALOGO DEL OBJ")]
    [SerializeField] cherrydev.DialogNodeGraph dialogo_obj;
    //Para encontrar los scripts de SceneManager
    private LoadScene load;
    private Preload preload;
    private GameObject script_load;

    //Para encontrar los scripts de DialogManager
    private Dialog dialog;
    private GameObject script_dialog;
    //public UnityEvent onInteract;

    //para acceder al obj que tiene el script le mandamos this.GameObject
    private void Awake()
    {
        if (script_load == null)
        {
            script_load = GameObject.Find("--SceneManagement--");
            load = script_load.GetComponent<LoadScene>();
            preload = script_load.GetComponent<Preload>();
        }
        if (script_dialog == null)
        {
            script_dialog = GameObject.Find("--DialogManager--");
            dialog = script_dialog.GetComponent<Dialog>();
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
            case "Interact_Scene":
                Debug.Log("Interaccionable por dialogo");
                dialog.EmpezarDialogo(dialogo_obj, a);
                break;
            default:
                Debug.Log("No hay nada");
                break;
        }


    }
    public void PuzzleFinished(bool val)
    {
        bool aux = val;
        if (aux)
        {
            dialog.SetBool("puzzleDone", val);
        }
        else
        {
            dialog.SetBool("puzzleDone", val);
        }
    }

}
