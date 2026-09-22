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
    public  bool isEnemy;  // ver si presiona para interaccion
    public  bool death;
    public bool inCombat;

    void Start()
    {
        opacidad(0f);
        interaction = false;
        text_canvas = false;
        canvas_visible = false;
        death = false;
        inCombat = false;
        //CanvasGroup hijo_canvas = transform.Find("Canvas Puzzle").GetComponent<CanvasGroup>();
        //grup = hijo_canvas;
        //TMP_Text hijo_texto = GetComponentInChildren< TMP_Text>();
        //text_interaccion = hijo_texto;
    }
    void Update()
    {
        //De dnd sale y adonde va
        ray = new Ray(pivot_personaje.transform.position, pivot_personaje.transform.forward);

        //canvas_visible = Input.GetKeyDown(KeyCode.P);
        canvas_visible = Input.GetKeyDown((KeyCode.Mouse0));

        Invoke(nameof(Interact), 1.0f);
    }
    public void Interact()
    {
        //detectar todos los objetos DELANTE del jugador
        //Collider[] colliders = Physics.OverlapBox(pivot.position, interactAreaSize);
        //Distancia máxima del ray, sino con Mathf.Infinity no tiene limite
        if (Physics.Raycast(ray, out hitInfo, 1f) && !death)
        {
            //Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);
            Interactable interactable;
            //Si no es null -> ha encontrado algo que tiene Interactable
            if (hitInfo.transform.gameObject.GetComponent<Interactable>() != null && !interaction)
            {
                if (!text_canvas)
                {
                    text_canvas = true;
                    textCanva(hitInfo.transform.gameObject);
                }
                //clicar boton para interaccionar
                //Interaccion
                
                if (isEnemy && !inCombat) //se pone en Texto canva
                {
                    inCombat = true;
                    interactable = hitInfo.transform.gameObject.GetComponent<Interactable>();

                    interactable.DetectObj(hitInfo.transform.gameObject);
                    
                }
                else if (!isEnemy && grup.alpha < 1f)
                {

                    opacidad(1f);
                }
                   
                if (canvas_visible) //Si se clica el boton
                {
                    Cursor.visible = true;
                    Debug.Log("Activar dialogo");

                    //Bool true asi no se sobreponen otras interacciones
                    interaction = true;

                    //Devuelve Obj que tiene Interactable
                    interactable = hitInfo.transform.gameObject.GetComponent<Interactable>();

                    interactable.DetectObj(hitInfo.transform.gameObject);
                }  

            }
            
        }
        else  //Cuando el Raycast no detecte nada
        {
            if (text_canvas)
                text_canvas = false;

            if (Cursor.visible)
                Cursor.visible = false;
            
            if (grup.alpha > 0f)
                opacidad(0f);
            
            if (interaction)
                interaction = false; //Volvemos a poner bool falso

            if (isEnemy)
                isEnemy = false;

            if (inCombat)
                inCombat = false;
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
                isEnemy = true;
                break;
            case "Interact_Scene":
                text_interaccion.text = "Inspeccionar";
                break;
            case "NPC":
                text_interaccion.text = "Hablar";
                break;
            case "Cofre":
                text_interaccion.text = "Abrir Cofre";
                break;
            case "Puerta":
                text_interaccion.text = "Abrir Puerta";
                break;
            case "PuertaMaestra":
                text_interaccion.text = "Abrir Puerta Maestra";
                break;
            default:
                //Debug.Log("No hay nada");
                break;
        }
    }
}
