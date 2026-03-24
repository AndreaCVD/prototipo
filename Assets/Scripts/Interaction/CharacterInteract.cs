using UnityEngine;

public class CharacterInteract : MonoBehaviour
{
    [SerializeField] Transform pivot;
    [SerializeField] Vector3 interactAreaSize = Vector3.one;

    public void Interact()
    {
        //detectar todos los objetos DELANTE del jugador
        Collider[] colliders = Physics.OverlapBox(pivot.position, interactAreaSize);

        foreach (Collider c in colliders)
        {
            Interactable interactable = c.GetComponent<Interactable>();

            //Si el objeto tiene el componente interactable, se hara una interacción
            if(interactable != null)
            {
                interactable.Interact();
                break;
            }
        } 
    }
}
