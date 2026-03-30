using UnityEngine;

public class CommandPanel : MonoBehaviour
{
    [SerializeField] LoadScene loadScene;
    //[SerializeField] Preload preload;
    [SerializeField] CommandManager commandManager;

    private void Awake()
    {
        //loadScene = GetComponent<LoadScene>();
    }
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
        loadScene.teleport();
        //preload.cambiarEscena("pruevas_prototipo");
    }
}
