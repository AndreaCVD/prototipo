using UnityEngine;

public class Viejo_Action : MonoBehaviour
{
    int ataque;
    [SerializeField] CommandManager commandManager;
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

    public void Ataque_1()
    {
        Debug.Log("Ataque 1 del viejo");
    }
    public void Ataque_2()
    {
        Debug.Log("Ataque 2 del viejo");
    }
}
