using UnityEngine;

public class Lava : MonoBehaviour
{
    Inventario a;
    bool pocion_Lava;

    void Start()
    {
        pocion_Lava = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !pocion_Lava)
        {
            Inventario a = other.GetComponent<Inventario>();
            if (!a.pocionLava())
            {
                a.restarVida();
            }
            else
                pocion_Lava = true;

        }
    }
}
