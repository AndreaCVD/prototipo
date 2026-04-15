using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    [Header("Pivot personaje")]
    [SerializeField] Transform pivot_personaje;
    //[SerializeField] Vector3 interactAreaSize = Vector3.one;
    [Header("Raycast")]
    RaycastHit hitInfo; //Informacion de cuando el raycast del personaje se encuentre con un obj
    Ray ray;
    [Header("Bools")]
    public bool interaction;
    void Start()
    {
        interaction = false;
    }
    void Update()
    {
        //De dnd sale y adonde va
        ray = new Ray(pivot_personaje.transform.position, pivot_personaje.transform.forward);

        Invoke(nameof(Interact), 2.0f);
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
                //Devuelve Obj que tiene Interactable
                Debug.Log(hitInfo.transform.gameObject.GetComponent<Interactable>());
                interactable = hitInfo.transform.gameObject.GetComponent<Interactable>();
                interactable.DetectObj(hitInfo.transform.gameObject);
                    
                //Bool true asi no se sobreponen otras interacciones
                interaction = true;
            }
                       
                //Debug.Log("No tiene Ineteractable");
            
        }
        else if(interaction) //Cuando el Raycast no detecte nada
        {
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
}
