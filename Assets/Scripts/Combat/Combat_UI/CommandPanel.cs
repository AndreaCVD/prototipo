using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandPanel : MonoBehaviour
{

    [SerializeField] CommandManager commandManager;

    private GameObject load_script;
    private LoadScene loadScene;

    private VisualElement root;
    VisualElement options_menu;
    VisualElement fuerza_options;

    //fila principal
    private Button btnFUE, btnCAR, btnINT, btnITEM;
    
    //fila ataque fuerza
    private Button btnDAGA, btnESPADA, btnBACK;

    //fila secundaria
    private Button btnItem, btnRun;


    void Start()
    {
        if (load_script == null)
        {
            load_script = GameObject.Find("--SceneManagement--");
            loadScene = load_script.GetComponent<LoadScene>();

        }

        var uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        btnFUE = root.Q<Button>("btn-FUE");
            btnDAGA = root.Q<Button>("btn-DAGA");
            btnESPADA = root.Q<Button>("btn-ESPADA");
            btnBACK = root.Q<Button>("btn-BACK");
        btnCAR = root.Q<Button>("btn-CAR");
        btnINT = root.Q<Button>("btn-INT");
        btnITEM = root.Q<Button>("btn-ITEM");
        btnItem = root.Q<Button>("btn-item");
        btnRun = root.Q<Button>("btn-huir");

        options_menu = root.Q<VisualElement>("option_menu");
        fuerza_options = root.Q<VisualElement>("atq-FUE");

        // eventos
        btnFUE.clicked += Fuerza;
            btnDAGA.clicked += Daga;
            btnESPADA.clicked += Espada;
            btnBACK.clicked += Back;

        btnINT.clicked += Intel;
        btnCAR.clicked += Carisma;
        btnItem.clicked += UsarItem;
        btnRun.clicked += Huir;

    }

    void OnDisable()
    {
        btnFUE.clicked -= Fuerza;
        btnINT.clicked -= Intel; //que es intel
        btnCAR.clicked -= Carisma;
        btnItem.clicked -= UsarItem;
        btnRun.clicked -= Huir;
    }

    //Boton Fuerza, se dice a command manager
    public void Fuerza()
    {
        //hacer visible los ataques de fuerza
        options_menu.style.display = DisplayStyle.None;
        fuerza_options.style.display = DisplayStyle.Flex;

        //commandManager.Fuerza(20);
        //Debug.Log("Ataque de fuerza");

    }
    public void Back()
    {
        options_menu.style.display = DisplayStyle.Flex;
        fuerza_options.style.display = DisplayStyle.None;
    }
    public void Daga()
    {
        commandManager.Fuerza(8);
        //Debug.Log("Ataque de fuerza");

    }
    public void Espada()
    {
        commandManager.Fuerza(12);
        //Debug.Log("Ataque de fuerza");

    }

    //Boton Inteligencia
    public void Intel()
    {
        commandManager.Inteligencia();
        //Debug.Log("Ha usado inteligencia");
    }
    //Boton Carisma
    public void Carisma()
    {
        commandManager.Carisma();
        //Debug.Log("Ha usado carisma");
    }

    // SECUNDARIAS
    //Boton Huir
    public void Huir()
    {
        //Debug.Log("Huir");
        loadScene.SalirCombate();
        //preload.cambiarEscena("pruevas_prototipo");
    }
    public void UsarItem()
    {
        Debug.Log("Usar ítem");
        // pendiente: abrir submenú de ítems
    }
}