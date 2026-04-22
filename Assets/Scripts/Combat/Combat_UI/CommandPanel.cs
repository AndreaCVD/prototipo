using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandPanel : MonoBehaviour
{
    [SerializeField] LoadScene loadScene;
    //[SerializeField] Preload preload;
    [SerializeField] CommandManager commandManager;

    public List<int> Armas = new List<int>();
    int daga = 4;
    int espada = 6;
    int conjuro = 12;

    [SerializeField] UIDocument uIDocument;
    private VisualElement root;

    void Start()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        List<VisualElement> allButtons = root.Query<VisualElement>(className: "combat_btns").ToList();

        foreach (VisualElement btn in allButtons)
        {

            btn.RegisterCallback<ClickEvent>(evt => {
                if (btn.name == "opt_fue")
                {
                    Fuerza();
                }
                else if (btn.name == "opt_int")
                {
                    Intel();
                }
                else if (btn.name == "opt_car")
                {
                    Carisma();
                }
                else if (btn.name == "opt_run")
                {
                    Huir();
                }
            });
        }
    }

    //Boton Fuerza, se dice a command manager
    public void Fuerza()
    {
        commandManager.Fuerza();
        Debug.Log("El ataque de fuerza ha acabo");
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
        loadScene.SalirCombate();
        //preload.cambiarEscena("pruevas_prototipo");
    }
}
