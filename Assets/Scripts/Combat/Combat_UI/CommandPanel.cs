using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandPanel : MonoBehaviour
{
    private GameObject load_script;
    private LoadScene loadScene;
    //[SerializeField] Preload preload;
    [SerializeField] CommandManager commandManager;

    public List<int> Armas = new List<int>();
    int daga = 4;
    int espada = 6;
    int conjuro = 12;

    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    List<VisualElement> allButtons;

    void Start()
    {
        if (load_script == null)
        {
            load_script = GameObject.Find("--SceneManagement--");
            loadScene = load_script.GetComponent<LoadScene>();

        }
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        allButtons = root.Query<VisualElement>(className: "combat_btns").ToList();

        foreach (VisualElement btn in allButtons)
        {

            btn.RegisterCallback<ClickEvent>(evt => {
                if (btn.name == "opt_fue")
                {
                    Debug.Log("Llega aqui?");
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
                    Debug.Log("Llega aqui?");
                    Huir();
                }
                else if (btn.name == "opt_attack")
                {

                    changeMenu("Attack");
                }
                else if (btn.name == "opt_goBack")
                {
                    Debug.Log("Llega aqui?");
                    changeMenu("Initial");
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
    public void changeMenu(string menuType)
    {
        foreach (VisualElement btn in allButtons)
        {
            switch (menuType)
            {
                case "Initial":
                    // En el menú inicial solo vemos Ataque y Huir
                    if (btn.name == "opt_attack" || btn.name == "opt_run")
                        btn.style.display = DisplayStyle.Flex;
                    else
                        btn.style.display = DisplayStyle.None;
                    break;

                case "Attack":
                    // En el menú de ataque vemos los ataques
                    if (btn.name == "opt_attack" || btn.name == "opt_run")
                        btn.style.display = DisplayStyle.None;
                    else
                        btn.style.display = DisplayStyle.Flex;
                    break;
            }
        }
    }
}
