using UnityEngine;

public class CommandPanel : MonoBehaviour
{
    [SerializeField] CommandManager commandManager;

    //Boton Fuerza, se dire a command manager
    public void Fuerza()
    {
        commandManager.Fuerza();
        //Debug.Log("Fuerza");
    }
    //Boton Inteligencia
    public void Intel()
    {
        Debug.Log("Inteligencia");
    }
    //Boton Carisma
    public void Carisma()
    {
        Debug.Log("Carisma");
    }
    //Boton Huir
    public void Huir()
    {
        Debug.Log("Huir");
    }
}
