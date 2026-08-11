using UnityEngine;

public class Caballero_Action : MonoBehaviour
{
    int ataque;

    public void Ataque_Aleatorio()
    {
        ataque = Random.Range(1, 4);

        switch (ataque)
        {
            case 1:
                Ataque_1();
                break;
            case 2:
                Ataque_2();
                break;
            default:
                Debug.Log("Error de lectura");
                break;
        }
    }

    void Ataque_1()
    {
        Debug.Log("Ataque 1 de mimic");
    }
    void Ataque_2()
    {
        Debug.Log("Ataque 2 de mimic");
    }
}
