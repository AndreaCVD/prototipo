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
        btnCAR = root.Q<Button>("btn-CAR");
        btnINT = root.Q<Button>("btn-INT");
        btnItem = root.Q<Button>("btn-item");
        btnRun = root.Q<Button>("btn-huir");

        // eventos
        btnFUE.clicked += Fuerza;
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
        commandManager.Fuerza();
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