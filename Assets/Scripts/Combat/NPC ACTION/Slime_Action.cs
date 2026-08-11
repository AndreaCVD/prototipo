using UnityEngine;

public class Slime_Action : MonoBehaviour
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
        Debug.Log("Ataque 1 de slime");
    }
    void Ataque_2()
    {
        Debug.Log("Ataque 2 de slime");
    }
}
