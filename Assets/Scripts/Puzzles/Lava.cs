using UnityEngine;

public class Lava : MonoBehaviour
{
    Inventario a;
    void OnTriggerStay(Collider other)
    {
            Debug.Log("enter");
        if (other.tag == "Player")
        {
            Inventario a = other.GetComponent<Inventario>();
            if (!a.pocionLava())
            {
                a.restarVida();
            }

        }
    }
}
