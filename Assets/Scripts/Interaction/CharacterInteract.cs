using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInteract : MonoBehaviour
{
    [Header("Canvas")] //Passar a UI
    public CanvasGroup grup;
    [SerializeField] TMP_Text text_interaccion;

    [Header("Pivot personaje")]
    [SerializeField] Transform pivot_personaje;
    //[SerializeField] Vector3 interactAreaSize = Vector3.one;
    [Header("Raycast")]
    RaycastHit hitInfo; //Informacion de cuando el raycast del personaje se encuentre con un obj
    Ray ray;
    [Header("Bools")]
    public bool interaction;
    public bool text_canvas;
    public bool canvas_visible;  // ver si presiona para interaccion
    void Start()
    {
        opacidad(0f);
        interaction = false;
        text_canvas = false;
        canvas_visible = false;
    }
    void Update()
    {
        //De dnd sale y adonde va
        ray = new Ray(pivot_personaje.transform.position, pivot_personaje.transform.forward);

        Invoke(nameof(Interact), 1.0f);
    }
    public void Interact()
    {
        //detectar todos los objetos DELANTE del jugador
        //Collider[] colliders = Physics.OverlapBox(pivot.position, interactAreaSize);
        //Distancia máxima del ray, sino con Mathf.Infinity no tiene limite
        if (Physics.Raycast(ray, out hitInfo, 1f))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);
            Interactable interactable;
            //Si no es null -> ha encontrado algo que tiene Interactable
            if (hitInfo.transform.gameObject.GetComponent<Interactable>() != null && !interaction)
            {
                if (!text_canvas)
                {
                    text_canvas=true;
                    textCanva(hitInfo.transform.gameObject);
                }
                //clicar boton para interaccionar
                //Salto
                canvas_visible = Input.GetKeyDown(KeyCode.P);
 
                opacidad(1f);
                if (canvas_visible)
                {

                    Debug.Log("Activar dialogo");

                    //Bool true asi no se sobreponen otras interacciones
                    interaction = true;

                    //Devuelve Obj que tiene Interactable
                    interactable = hitInfo.transform.gameObject.GetComponent<Interactable>();

                    interactable.DetectObj(hitInfo.transform.gameObject);
                }  

            }
            else
            {
                text_canvas = false;
                opacidad(0f);
            }           
                //Debug.Log("No tiene Ineteractable");
            
        }
        else  //Cuando el Raycast no detecte nada
        {
            text_canvas = false;
            opacidad(0f);
            if (interaction)
                interaction = false; //Volvemos a poner bool falso
        }
            //foreach (Collider c in colliders)
            //{
            //    Interactable interactable = c.GetComponent<Interactable>();

            //    //Si el objeto tiene el componente interactable, se hara una interacción
            //    if(interactable != null)
            //    {
            //        interactable.Interact();
            //        break;
            //    }
            //} 
        
    }
    void opacidad(float nueva_opacidad)
    {
        grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
    void textCanva(GameObject obj)
    {
        switch (obj.tag)
        {
            case "Enemy":
                text_interaccion.text = "P - Iniciar Pelea";
                break;
            case "Interact_Scene":
                text_interaccion.text = "P - Inspeccionar";
                break;
            case "NPC":
                text_interaccion.text = "P - para hablar";
                break;
            case "Cofre":
                text_interaccion.text = "P - Abrir Cofre ";
                break;
            case "Puerta":
                text_interaccion.text = "P - Abrir Puerta";
                break;
            case "PuertaMaestra":
                text_interaccion.text = "P - Abrir Puerta Maestra";
                break;
            default:
                //Debug.Log("No hay nada");
                break;
        }
    }
}
