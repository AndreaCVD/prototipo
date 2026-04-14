using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    [SerializeField] Transform pivot_personaje;
    [SerializeField] Vector3 interactAreaSize = Vector3.one;

    RaycastHit hitInfo; //Informacion de cuando el raycast del personaje se encuentre con un obj
    Ray ray;

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
        Physics.Raycast(ray, out hitInfo, 10f);

        Interactable interactable;

        if (hitInfo.transform.gameObject.GetComponent<Interactable>() != null)
        {
            Debug.Log(hitInfo.transform.gameObject.GetComponent<Interactable>()); //Devuelve Obj que tiene Interactable
            interactable = hitInfo.transform.gameObject.GetComponent<Interactable>();
            interactable.DetectObj(hitInfo.transform.gameObject);
        }
        else
        {
            Debug.Log("No tiene Ineteractable");
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
