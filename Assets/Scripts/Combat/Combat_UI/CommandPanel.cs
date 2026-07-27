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
    VisualElement fuerza_options, intel_options;

    //fila principal
    private Button btnFUE, btnCAR, btnINT, btnITEM;
    
    //fila ataque fuerza
    private Button btnDAGA, btnESPADA, btnBACK;
    //fila ataque fuerza
    private Button btnINMOV, btnESCUDO, btnBACK_intel;

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
            btnINMOV = root.Q<Button>("btn-INMOV");
            btnESCUDO = root.Q<Button>("btn-ESCUDO");
            btnBACK_intel = root.Q<Button>("intel-BACK");
        btnITEM = root.Q<Button>("btn-ITEM");
        btnItem = root.Q<Button>("btn-item");
        btnRun = root.Q<Button>("btn-huir");

        options_menu = root.Q<VisualElement>("option_menu");
        fuerza_options = root.Q<VisualElement>("atq-FUE");
        intel_options = root.Q<VisualElement>("atq-INTEL");

        // eventos
        btnFUE.clicked += Fuerza;
            btnDAGA.clicked += Daga;
            btnESPADA.clicked += Espada;
            btnBACK.clicked += Back;

        btnINT.clicked += Intel;
            btnINMOV.clicked += Inmovilizar;
            btnESCUDO.clicked += Escudo;
            btnBACK_intel.clicked += Back_intel;

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
        options_menu.style.display = DisplayStyle.None;
        intel_options.style.display = DisplayStyle.Flex;
        //commandManager.Inteligencia();
        //Debug.Log("Ha usado inteligencia");
    }
    public void Inmovilizar()
    {
        commandManager.Inteligencia(12);
        //Debug.Log("Ataque de fuerza");

    }
    public void Escudo()
    {
        commandManager.Inteligencia(4);
        //Debug.Log("Ataque de fuerza");

    }
    public void Back_intel()
    {
        options_menu.style.display = DisplayStyle.Flex;
        intel_options.style.display = DisplayStyle.None;
        //commandManager.Inteligencia();
        //Debug.Log("Ha usado inteligencia");
    }

    //Boton Carisma
    public void Carisma()
    {
        commandManager.Carisma(20);
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