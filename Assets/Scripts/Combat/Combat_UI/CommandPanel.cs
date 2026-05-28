using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandPanel : MonoBehaviour
{
    [SerializeField] CommandManager commandManager;

    private GameObject load_script;
    private LoadScene loadScene;

    private VisualElement root;

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

        btnFUE = root.Q<Button>("btn-FUE");
        btnCAR = root.Q<Button>("btn-CAR");
        btnINT = root.Q<Button>("btn-INT");
        btnItem = root.Q<Button>("btn-item");
        btnDefend = root.Q<Button>("btn-defender");
        btnRun = root.Q<Button>("btn-huir");

        // eventos
        btnFUE.clicked += Fuerza;
        btnINT.clicked += Intel;
        btnCAR.clicked += Carisma;
        //Por implementar
        btnItem.clicked += UsarItem;
        btnDefend.clicked += Defender;

        btnRun.clicked += Huir;

    }

    void OnDisable()
    {
        btnFUE.clicked -= Fuerza;
        btnINT.clicked -= Intel; //que es intel
        btnCAR.clicked -= Carisma;
        //Por implementar
        btnItem.clicked -= UsarItem;
        btnDefend.clicked -= Defender;

        btnRun.clicked -= Huir;
    }

    //Boton Fuerza, se dice a command manager
    public void Fuerza()
    {
        commandManager.Fuerza();
        Debug.Log("Ataque de fuerza");

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
}