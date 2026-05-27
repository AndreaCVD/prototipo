using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandPanel : MonoBehaviour
{
    [SerializeField] CommandManager commandManager;

    private GameObject load_script;
    private LoadScene loadScene;

    private VisualElement root;
    private VisualElement combatScreen;
    private VisualElement gameHud;

    //fila principal
    private Button btnFUE, btnCAR, btnINT;

    //fila secundaria
    private Button btnItem, btnDefend, btnRun;

    public List<int> Armas = new List<int>();
    int daga = 4;
    int espada = 6;
    int conjuro = 12;

    void Start()
    {
        if (load_script == null)
        {
            load_script = GameObject.Find("--SceneManagement--");
            loadScene = load_script.GetComponent<LoadScene>();

        }

        var uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        // busca el UIDocument del HUD en todas las escenas cargadas
        UIDocument[] allDocs = FindObjectsByType<UIDocument>(FindObjectsSortMode.None);
        foreach (UIDocument doc in allDocs)
        {
            if (doc.gameObject.name == "UI_HUB")   // ← nombre exacto del GameObject del HUD
            {
                gameHud = doc.rootVisualElement;
                break;
            }
        }

        if (gameHud == null)
            Debug.LogWarning("CommandPanel: no se encontró UI_HUB en la escena.");

        //refs
        combatScreen = root.Q<VisualElement>("combat-screen");

        btnFUE = root.Q<Button>("btn-FUE");
        btnCAR = root.Q<Button>("btnCAR");
        btnINT = root.Q<Button>("btnINT");
        btnItem = root.Q<Button>("btn-item");
        btnDefend = root.Q<Button>("btn-defender");
        btnRun = root.Q<Button>("btn-huir");

        // eventos
        btnFUE.clicked += Fuerza;
        btnCAR.clicked += Intel;
        btnINT.clicked += Carisma;
        //Por implementar
        btnItem.clicked += UsarItem;
        btnDefend.clicked += Defender;

        btnRun.clicked += Huir;

        MostrarCombate();

    }

    void OnDisable()
    {
        btnFUE.clicked -= Fuerza;
        btnCAR.clicked -= Intel; //que es intel
        btnINT.clicked -= Carisma;
        //Por implementar
        btnItem.clicked -= UsarItem;
        btnDefend.clicked -= Defender;

        btnRun.clicked -= Huir;

        OcultarCombate();
    }

    //Boton Fuerza, se dice a command manager
    public void Fuerza()
    {
        commandManager.Fuerza();
        Debug.Log("Ataque de fuerza");
        OcultarCombate();

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

    // SECUNDARIAS
    //Boton Huir
    public void Huir()
    {
        Debug.Log("Huir");
        loadScene.SalirCombate();
        //preload.cambiarEscena("pruevas_prototipo");
    }
    public void UsarItem()
    {
        Debug.Log("Usar ítem");
        // pendiente: abrir submenú de ítems
    }
    public void Defender()
    {
        Debug.Log("Defender");
        // pendiente: commandManager.Defender() para que pueda usar algo para defenderse(? ya sea un item o un stat
    }

    public void MostrarCombate()
    {
        Debug.Log("mostrar combate");
        combatScreen.style.display = DisplayStyle.Flex;
        if (gameHud != null)
            gameHud.style.display = DisplayStyle.None;
    }

    public void OcultarCombate()
    {
        Debug.Log("mostrar combate");

        combatScreen.style.display = DisplayStyle.None;
        if (gameHud != null)
            gameHud.style.display = DisplayStyle.Flex;
    }
}