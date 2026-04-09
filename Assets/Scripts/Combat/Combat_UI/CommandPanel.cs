using UnityEngine;

public class CommandPanel : MonoBehaviour
{
    [SerializeField] LoadScene loadScene;
    //[SerializeField] Preload preload;
    [SerializeField] CommandManager commandManager;


    //Boton Fuerza, se dice a command manager
    public void Fuerza()
    {
        commandManager.Fuerza();
        Debug.Log("Ha usado fuerza");
    }
    //Boton Inteligencia
    public void Intel()
    {
        commandManager.Inteligencia();
        Debug.Log("Ha usado inteligencia");
    }
    //Boton Carisma
    public void Carisma()
    {
        commandManager.Carisma();
        Debug.Log("Ha usado carisma");
    }
    //Boton Huir
    public void Huir()
    {
        Debug.Log("Huir");
        loadScene.ChangeScene("pruevas_prototipo");
        //preload.cambiarEscena("pruevas_prototipo");
    }
}
