using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//Cualquier obj que sea interactuable tendra este script
public class Interactable : MonoBehaviour
{
    [Header("EL DIALOGO DEL OBJ")]
    [SerializeField] cherrydev.DialogNodeGraph dialogo_obj;
    [Header("FICHA DEL OBJ")]
    public Parameters ficha_obj;
    //Para encontrar los scripts de SceneManager
    private LoadScene load;
    private Preload preload;
    private GameObject script_load;
    //Para encontrar los scripts de Inventario
    private Inventario inventario;
    private GameObject script_inventario;
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
        if (script_inventario == null)
        {
            script_inventario = GameObject.Find("personaje");
            inventario = script_inventario.GetComponent<Inventario>();
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
                preload.CombatOpponent( a );
                load.Combat(a);
                break;
            //case "Puzzle":
            //    //Debug.Log("This is a Puzzle");
            //    break;
            case "Interact_Scene":
                dialog.EmpezarDialogo(dialogo_obj, a);
                break;
            case "NPC":
                dialog.EmpezarDialogo(dialogo_obj, a);
                break;
            case "Cofre":
                inventario.CofreKey(a, dialogo_obj);
                break;
            case "Puerta":
                inventario.PuertaKey(a, dialogo_obj);
                break;
            case "PuertaMaestra":
                inventario.PuertaMaestraKey(a, dialogo_obj);
                break;
            default:
                //Debug.Log("No hay nada");
                break;
        }


    }
    public void PuzzleFinished(bool val)
    {
        bool aux = val;
        preload.puzzleTrue(this.gameObject.name);
        //if (aux)
        //{
        //    dialog.SetBool("puzzleDone", val);
        //}
        //else
        //{
        //    dialog.SetBool("puzzleDone", val);
        //}
    }

}
