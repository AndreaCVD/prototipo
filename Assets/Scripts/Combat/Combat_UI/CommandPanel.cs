using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CommandPanel : MonoBehaviour
{
    [Header("Ficha personaje")]
    [SerializeField] Parameters protagonista;
    [Header("Iconos Inventario")]
    [SerializeField] Sprite iconoLlave;
    [SerializeField] Sprite iconoLlaveMaestra;
    [SerializeField] Sprite iconoPocionVida;
    [SerializeField] Sprite iconoDaga;
    [SerializeField] Sprite iconoEspada;
    [SerializeField] Sprite iconoPocionLava;

    [SerializeField] CommandManager commandManager;

    private GameObject load_script;
    private LoadScene loadScene;

    private VisualElement root;
    VisualElement options_menu;
    VisualElement fuerza_options, intel_options, inventoryGrid;

    //fila principal
    private Button btnFUE, btnCAR, btnINT, btnITEM;
    
    //fila ataque fuerza
    private Button btnDAGA, btnESPADA, btnBACK;
    //fila ataque intel
    private Button btnINMOV, btnESCUDO, btnBACK_intel;
    //fila ataque item
    private Button btnBACK_item, btn_slot_2;

    //fila secundaria
    private Button btnItem, btnRun;
    //contadores inventory
    private int llaves, llaveMaestra, daga, espada, pocionVida, pocionLava;

    void Start()
    {
        llaves = 0;
        llaveMaestra = 0;
        espada = 0;
        daga = 0;
        pocionVida = 0;
        pocionLava = 0;

        if (load_script == null)
        {
            load_script = GameObject.Find("--SceneManagement--");
            loadScene = load_script.GetComponent<LoadScene>();

        }

        var uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        //Botones
        btnFUE = root.Q<Button>("btn-FUE");
            btnDAGA = root.Q<Button>("btn-DAGA");
            btnESPADA = root.Q<Button>("btn-ESPADA");
            btnBACK = root.Q<Button>("btn-BACK");
        //Botones inteligencia
        btnINT = root.Q<Button>("btn-INT");
            btnINMOV = root.Q<Button>("btn-INMOV");
            btnESCUDO = root.Q<Button>("btn-ESCUDO");
            btnBACK_intel = root.Q<Button>("intel-BACK");
        //Botones carisma
        btnCAR = root.Q<Button>("btn-CAR");
        //Botones Bolsa Items
        btnITEM = root.Q<Button>("btn-ITEM");
            btnBACK_item = root.Q<Button>("btn-ITEM-BACK");
            btn_slot_2 = root.Q<Button>("btn-slot-2"); //pocion vida

        btnRun = root.Q<Button>("btn-huir");
        //Visual Elements
        options_menu = root.Q<VisualElement>("option_menu");
        fuerza_options = root.Q<VisualElement>("atq-FUE");
        intel_options = root.Q<VisualElement>("atq-INTEL");
        inventoryGrid = root.Q<VisualElement>("inventory-grid");
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

        btnRun.clicked += Huir;

        //inventary
        btnITEM.clicked += Abrir_Inventario;
            btnBACK_item.clicked += Back_item;
            btn_slot_2.clicked += () => UsarItem(2);

    }

    void Update()
    {
        //inventary
        if (!areListEqual())
        {
            SetInventario();
        }
    }
    void OnDisable()
    {
        btnFUE.clicked -= Fuerza;
        btnINT.clicked -= Intel; //que es intel
        btnCAR.clicked -= Carisma;
        btnITEM.clicked -= Abrir_Inventario;
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

    void Abrir_Inventario()
    {
        Debug.Log("Abrir Inventario");
        options_menu.style.display = DisplayStyle.None;
        inventoryGrid.style.display = DisplayStyle.Flex;
    }
    void Back_item()
    {
        Debug.Log("Cerrar Inventario");
        options_menu.style.display = DisplayStyle.Flex;
        inventoryGrid.style.display = DisplayStyle.None;
    }
    void UsarItem(int slot)
    {
        //vemos que item es y lo sacamos del inventario
        switch (slot)
        {
            case 0:
                break;
             case 1:
                break;
             case 2: //Pocion de vida
                Debug.Log("El jugador usa una pocion, recupera 10 de vida");
                int vida = protagonista.stats.values[3].value;
                if (vida > 0 && vida < 30) //MAX vida - 10
                {
                    protagonista.stats.values[3].value -= 1;
                }
                protagonista.Inventario.PocionVida.RemoveAt(protagonista.Inventario.PocionVida.Count - 1);
                break;
             case 3:
                break;
             case 4:
                break;
             case 5:
                break;
            default:
                break;
        }
        //actualizamos el inventario
    }
    bool areListEqual()
    {
        // Null check del inventario completo
        if (protagonista == null || protagonista.Inventario == null) return false;

        // Null check de cada lista antes de llamar .Count()
        if (protagonista.Inventario.Llave == null) return false;
        if (protagonista.Inventario.LlaveMaestra == null) return false;
        if (protagonista.Inventario.PocionVida == null) return false;
        if (protagonista.Inventario.Daga == null) return false;
        if (protagonista.Inventario.PocionLava == null) return false;
        if (protagonista.Inventario.Espada == null) return false;

        if (protagonista.Inventario.Llave.Count != llaves) return false;
        if (protagonista.Inventario.LlaveMaestra.Count != llaveMaestra) return false;
        if (protagonista.Inventario.PocionVida.Count != pocionVida) return false;
        if (protagonista.Inventario.PocionLava.Count != pocionVida) return false;
        if (protagonista.Inventario.Daga.Count != daga) return false;
        if (protagonista.Inventario.Espada.Count != espada) return false;

        return true;
    }
    void SetInventario()
    {
        // Null check antes de acceder a las listas
        if (protagonista == null || protagonista.Inventario == null) return;
        if (protagonista.Inventario.Llave == null) return;
        if (protagonista.Inventario.LlaveMaestra == null) return;
        if (protagonista.Inventario.PocionVida == null) return;
        if (protagonista.Inventario.PocionLava == null) return;
        if (protagonista.Inventario.Daga == null) return;
        if (protagonista.Inventario.Espada == null) return;

        llaves = protagonista.Inventario.Llave.Count;
        llaveMaestra = protagonista.Inventario.LlaveMaestra.Count;
        pocionVida = protagonista.Inventario.PocionVida.Count;
        pocionLava = protagonista.Inventario.PocionLava.Count;
        daga = protagonista.Inventario.Daga.Count;
        espada = protagonista.Inventario.Espada.Count;

        SetSlot(0, llaves > 0 ? iconoLlave : null, llaves);
        SetSlot(1, llaveMaestra > 0 ? iconoLlaveMaestra : null, llaveMaestra);
        SetSlot(2, pocionVida > 0 ? iconoPocionVida : null, pocionVida);
        SetSlot(3, daga > 0 ? iconoDaga : null, daga);
        SetSlot(4, espada > 0 ? iconoEspada : null, espada);
        SetSlot(5, pocionLava > 0 ? iconoPocionLava : null, pocionLava);
    }
    void SetSlot(int index, Sprite icono, int cantidad)
    {
        //poner el icono y numero
        var slotIcon = root.Q<VisualElement>($"slot-{index}-icon");
        var slotBadge = root.Q<Label>($"slot-{index}-badge");
        var slot = root.Q<VisualElement>($"slot-{index}");

        if (icono != null)
        {
            //si antes estaba vacio
            bool esNuevo = !slot.ClassListContains("inv-slot-active");
            slotIcon.style.backgroundImage = new StyleBackground(icono);
            slot.AddToClassList("inv-slot--active");

            //if (esNuevo) MostrarNotificacion(icono);

        }
        else
        {
            slotIcon.style.backgroundImage = StyleKeyword.None;
            slot.RemoveFromClassList("inv-slot--active");
        }

        slotBadge.text = cantidad.ToString();
        slotBadge.style.display = cantidad > 0
            ? DisplayStyle.Flex
            : DisplayStyle.None;
    }
}