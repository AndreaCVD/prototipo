using UnityEngine;

public class Lava : MonoBehaviour
{
    Inventario a;
    void OnTriggerStay(Collider other)
    {
            Debug.Log("enter");
        if (other.tag == "Player")
        {
            Debug.Log("player");

            a = other.GetComponent<Inventario>();
            a.restarVida();
        }
    }
}
