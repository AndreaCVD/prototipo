using UnityEngine;

public class TPRooms : MonoBehaviour
{
    [Header("Destino del TP")]
    [SerializeField] private Transform destination;

    [Header("Filtro (opcional)")]
    [SerializeField] private string tagFilter = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (!string.IsNullOrEmpty(tagFilter) && !other.CompareTag(tagFilter))
            return;

        Teleport(other.gameObject);
    }

    private void Teleport(GameObject obj)
    {
        if (destination == null)
        {
            Debug.LogWarning("TeleportTrigger: no hay destino asignado en " + gameObject.name);
            return;
        }

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // fuera velocidad y fisicas raras
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.MovePosition(destination.position);
        }
        else
        {
            obj.transform.position = destination.position;
        }

        obj.transform.rotation = destination.rotation;
        Debug.Log($"{obj.name} teleportado a {destination.name}");
    }
}
