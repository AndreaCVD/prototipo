using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; // Imprescindible para UI Toolkit
using System.Linq;
using System.Collections; //Comparacion de Listas
using Cursor = UnityEngine.Cursor;

public class stats_UI : MonoBehaviour
{
    private InputHandler escenaState;

    [Header("Ficha personaje")]
    [SerializeField] Parameters protagonista;

    [Header("Iconos Inventario")]
    [SerializeField] Sprite iconoLlave;
    [SerializeField] Sprite iconoLlaveMaestra;
    [SerializeField] Sprite iconoPocionVida;
    [SerializeField] Sprite iconoDaga;
    [SerializeField] Sprite iconoEspada;
    [SerializeField] Sprite iconoPocionLava;
    [SerializeField] Sprite iconoMonedas;
    [SerializeField] Sprite iconoArmaduraCuero;
    [SerializeField] Sprite iconoArmaduraMalla;
    [SerializeField] Sprite iconoVacio;

    //ref del UI
    private VisualElement root;
    private VisualElement hud_top, hud_bottom_right, hud_bottom;

    private IntegerField fieldFUE, fieldINT, fieldCAR, fieldLIFE, fieldCA;
    private VisualElement heartFill;
    private int maxLife;

    //del inventario
    private Button btn_prueva;

    //inv en el UI
    private VisualElement inventoryGrid;
    private Button btnInventory;
    private VisualElement itemNotification;
    private VisualElement notifIcon;
    private Coroutine notifCoroutine;

    private VisualElement info_items_block;
    private VisualElement item_0, item_1, item_2, item_3, item_4, item_5;
    private VisualElement choose_ca;
    private VisualElement btn_cuero, btn_malla, ca_vaciar;

    //contadores inventory
    private int llaves, llaveMaestra;
    private int cuero, malla;
    //private int daga;
    //private int espada;
    private int pocionVida;
    private int pocionLava;
    private int monedas;
    private string armadura;
    private bool inCombat, inInventary;

    private int evt_btn;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        hud_top = root.Q("hud-top").Q<VisualElement>();
        hud_bottom_right = root.Q("hud-bottom-right").Q<VisualElement>();
        hud_bottom = root.Q("hud-bottom").Q<VisualElement>();
        //stats
        fieldFUE = root.Q("FUE").Q<IntegerField>();
        fieldINT = root.Q("INT").Q<IntegerField>();
        fieldCAR = root.Q("CAR").Q<IntegerField>();
        fieldLIFE = root.Q("int_life").Q<IntegerField>();
        fieldCA = root.Q("int_CA").Q<IntegerField>();
        heartFill = root.Q<VisualElement>("heart-fill");

        btn_prueva = root.Q<Button>("btn_slot_0");
        btn_prueva.RegisterCallback<MouseEnterEvent>(OnButtonHoverEnter);
        btn_prueva.RegisterCallback<MouseLeaveEvent>(OnButtonHoverExit);

        //inventary
        inventoryGrid = root.Q<VisualElement>("inventory-grid");
        btnInventory = root.Q<Button>("btn-inventory");
        btnInventory.clicked += ToggleInventary;
        inventoryGrid.style.display = DisplayStyle.None;

        itemNotification = root.Q<VisualElement>("item-notification");
        notifIcon = root.Q<VisualElement>("notif-icon");
        //Debug.Log(inventoryGrid);
        //Debug.Log(notifIcon);

        choose_ca = root.Q<VisualElement>("slot_5_choose");
        btn_cuero = root.Q<VisualElement>($"btn_slot_{51}");
        btn_malla = root.Q<VisualElement>($"btn_slot_{52}");
        ca_vaciar = root.Q<VisualElement>($"btn_slot_{53}");
        btn_cuero.RegisterCallback<FocusEvent>(OnFocusChanged);
        btn_malla.RegisterCallback<FocusEvent>(OnFocusChanged);
        ca_vaciar.RegisterCallback<FocusEvent>(OnFocusChanged);

        info_items_block = root.Q<VisualElement>("info_items_inventario");
        item_0 =  root.Q<VisualElement>($"btn_slot_{0}");
        item_1 =  root.Q<VisualElement>($"btn_slot_{1}");
        item_2 =  root.Q<VisualElement>($"btn_slot_{2}");
        item_3 =  root.Q<VisualElement>($"btn_slot_{3}");
        item_4 =  root.Q<VisualElement>($"btn_slot_{4}");
        item_5 =  root.Q<VisualElement>($"btn_slot_{5}");

        item_0.RegisterCallback<FocusEvent>(OnFocusChanged);
        item_1.RegisterCallback<FocusEvent>(OnFocusChanged);
        item_2.RegisterCallback<FocusEvent>(OnFocusChanged);
        item_3.RegisterCallback<FocusEvent>(OnFocusChanged);
        item_4.RegisterCallback<FocusEvent>(OnFocusChanged);
        item_5.RegisterCallback<FocusEvent>(OnFocusChanged);
        //input A --> ir a la izquierda
        //Input.GetKeyDown((KeyCode.A).clicked += MoveFocused;
    }

    private void OnDisable()
    {
        btnInventory.clicked -= ToggleInventary;
    }

    // Mouse en el inventario
    private void OnButtonHoverEnter(MouseEnterEvent evt)
    {
        Debug.Log("Mouse ha entrado");
        // Add your hover logic here, e.g., change color, show tooltip
    }
    private void OnButtonHoverExit(MouseLeaveEvent evt)
    {
        Debug.Log("Mouse ha salido");
    }

    void Start()
    {
        escenaState = GameObject.Find("personaje").GetComponent<InputHandler>();

        llaves = 0;
        llaveMaestra = 0;
        //espada = 0;
        //daga = 0;
        pocionVida = 0;
        pocionLava = 0;
        monedas = 0;
        cuero = 0;
        malla = 0;
        armadura = null;
        inInventary = false;
        //Seteamos valores, int -> string
        int f = protagonista.stats.Get(PersonajesStats.Fuerza);
        int i = protagonista.stats.Get(PersonajesStats.Inteligencia);
        int c = protagonista.stats.Get(PersonajesStats.Carisma);
        int ca = protagonista.stats.Get(PersonajesStats.ClaseArmadura);
        //h = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();
        //int h = protagonista.stats.Get(PersonajesStats.Constitucion);

        maxLife = protagonista.stats.Get(PersonajesStats.Max_Vida);
        info_items_block.style.display = DisplayStyle.None; //si esta flex bloquea mouse

        SetFuerza(f);
        SetIntel(i);
        SetCarisma(c);
        SetCA(ca);
        SetInventario();
    }

    void Update()
    {

        //health
        int aux = protagonista.stats.Get(PersonajesStats.Constitucion);
        if (fieldLIFE.value != aux)
        {
            SetConstitucion(aux);
        }

        //inventary
        if (!areListEqual())
        {
            SetInventario();
        }

        //CA
        int ca = protagonista.stats.Get(PersonajesStats.ClaseArmadura);
        if (fieldCA.value != ca)
        {
            SetCA(ca);
        }
        //abrir/cerrar con la tecla I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventary();
        }
        if (Input.GetKeyDown(KeyCode.Space) && inInventary)
        {
            Use_Item();
        }
    }
    // Ocultar o visibilizar partes de la UI
    public void Iniciar_Combate()
    {
        inCombat = true;
        //arriba, el puzle, y inventory grid
        hud_top.style.display = DisplayStyle.None;
        hud_bottom_right.style.display = DisplayStyle.None;

        inventoryGrid.style.display = DisplayStyle.None;
    }
    public void Acabar_Combate()
    {
        inCombat = false;

        hud_top.style.display = DisplayStyle.Flex;
        hud_bottom_right.style.display = DisplayStyle.Flex;

    }
    public void DesPausa()
    {
        hud_top.style.display = DisplayStyle.Flex;
        hud_bottom_right.style.display = DisplayStyle.Flex;
        hud_bottom.style.display = DisplayStyle.Flex;
    }
    public void Pausa()
    {
        hud_top.style.display = DisplayStyle.None;
        hud_bottom_right.style.display = DisplayStyle.None;
        hud_bottom.style.display = DisplayStyle.None;

        inventoryGrid.style.display = DisplayStyle.None;
        info_items_block.style.display = DisplayStyle.None;

        //VisualElement a = root.Q<VisualElement>("slot_5_choose");
        //a.style.display = DisplayStyle.None;
    }
    // Inventario
    void OnFocusChanged(FocusEvent evt)
    {

        //desactivamos todos
        for (int i = 0; i <= 5; i++)
        {
            VisualElement aux = root.Q<VisualElement>($"info_slot_{i}");
            aux.style.display = DisplayStyle.None;
        }

        if (evt.target == item_0) //si es focus, activamos la info
        {
            evt_btn = 0;

            VisualElement info = root.Q<VisualElement>("info_slot_0");
            info.style.display = DisplayStyle.Flex;
            //item_0.style.backgroundColor =
            //    item_0.style.backgroundColor.value.a > 0 ? new StyleColor(Color.clear) : new StyleColor(Color.gray);
        }
    
        else if (evt.target == item_1)
        {
            evt_btn = 1;

            VisualElement info = root.Q<VisualElement>("info_slot_1");
            info.style.display = DisplayStyle.Flex;
        }
        else if (evt.target == item_2)
        {
            evt_btn = 2;

            VisualElement info = root.Q<VisualElement>("info_slot_2");
            info.style.display = DisplayStyle.Flex;
        }
        else if (evt.target == item_3)
        {
            evt_btn = 3;
            VisualElement info = root.Q<VisualElement>("info_slot_3");
            info.style.display = DisplayStyle.Flex;
        }
        else if (evt.target == item_4)
        {
            evt_btn = 4;
            VisualElement info = root.Q<VisualElement>("info_slot_4");
            info.style.display = DisplayStyle.Flex;
        }
        else if (evt.target == item_5)
        {
            evt_btn = 5;
            VisualElement info = root.Q<VisualElement>("info_slot_5");
            info.style.display = DisplayStyle.Flex;
        }
        else if (evt.target == btn_cuero)
        {
            evt_btn = 51;
        }
        else if (evt.target == btn_malla)
        {
            evt_btn = 52;
        }
        else if (evt.target == ca_vaciar)
        {
            evt_btn = 53;
        }

    }
    void ToggleInventary()
    {
        if (!inCombat)
        {
            bool isDisplayed = inventoryGrid.style.display == DisplayStyle.Flex;
            inventoryGrid.style.display = isDisplayed ? DisplayStyle.None : DisplayStyle.Flex;

            if (!isDisplayed)
            {
                info_items_block.style.display = DisplayStyle.Flex;
                inInventary = true;
                escenaState.ScenePause(true); //true, se para
                //Cursor.lockState = CursorLockMode.None;
                root.Q<VisualElement>("btn_slot_0").Focus();

            }
            else
            {
                info_items_block.style.display = DisplayStyle.None;
                VisualElement a = root.Q<VisualElement>("slot_5_choose");
                a.style.display = DisplayStyle.None;

                inInventary = false;
                escenaState.ScenePause(false);
            }
        }
    }

    void Use_Item()
    {
        Debug.Log(evt_btn);
        if (evt_btn == 0) // llave
        {
            Debug.Log("Usar item llave");

        }
        else if (evt_btn == 1) // llave maestra
        {
            Debug.Log("Usar item llave maestra");

        }
        else if (evt_btn == 2) // pocion vida
        {
            int vida = protagonista.stats.values[3].value;
            if (protagonista.Inventario.PocionVida.Count != 0)
            {
                if (vida > 0 && vida <= maxLife - 10) //MAX vida - 10
                {
                    protagonista.stats.values[3].value += 10;
                    protagonista.Inventario.PocionVida.RemoveAt(protagonista.Inventario.PocionVida.Count - 1);
                }
                else if (vida > 0 && vida < maxLife)
                {
                    protagonista.stats.values[3].value = maxLife;
                    protagonista.Inventario.PocionVida.RemoveAt(protagonista.Inventario.PocionVida.Count - 1);
                }
                else
                    Debug.Log("Tienes vida maxima");
            }
        }
        else if (evt_btn == 3) // pocion lava
        {
            Debug.Log("Usar item pocion lava");

        }
        else if (evt_btn == 4) // monedas
        {
            Debug.Log("Usar item monedas");

        }
        else if (evt_btn == 5) // armadura
        {
            VisualElement info = root.Q<VisualElement>("info_slot_5");
            info.style.display = DisplayStyle.None;

            VisualElement aux = root.Q<VisualElement>("slot_5_choose");
            aux.style.display = DisplayStyle.Flex;
            root.Q<VisualElement>($"btn_slot_{51}").Focus();
        }
        else if (evt_btn == 51) //cuero
        {
            VisualElement info = root.Q<VisualElement>("info_slot_5");
            info.style.display = DisplayStyle.Flex;
            VisualElement aux = root.Q<VisualElement>("slot_5_choose");
            aux.style.display = DisplayStyle.None;
            root.Q<VisualElement>("btn_slot_5").Focus();

            protagonista.Inventario.Armadura = "cuero";
            SetArmadura();
        }
        else if (evt_btn == 52) //malla
        {
            VisualElement info = root.Q<VisualElement>("info_slot_5");
            info.style.display = DisplayStyle.Flex;
            VisualElement aux = root.Q<VisualElement>("slot_5_choose");
            aux.style.display = DisplayStyle.None;
            root.Q<VisualElement>("btn_slot_5").Focus();

            protagonista.Inventario.Armadura = "malla";
            SetArmadura();
        }
        else if (evt_btn == 53)
        {
            protagonista.Inventario.Armadura = "no";
            SetArmadura();
        }
    }


    bool areListEqual()
    {
        // Null check del inventario completo
        if (protagonista == null || protagonista.Inventario == null) return false;

        // Null check de cada lista antes de llamar .Count()
        if (protagonista.Inventario.Llave == null) return false;
        if (protagonista.Inventario.LlaveMaestra == null) return false;
        if (protagonista.Inventario.PocionVida == null) return false;
        //if (protagonista.Inventario.Daga == null) return false;
        if (protagonista.Inventario.PocionLava == null) return false;
        if (protagonista.Inventario.Monedas == 0) return false;
        //if (protagonista.Inventario.Espada == null) return false;
        if (protagonista.Inventario.Armadura == null) return false;

        if (protagonista.Inventario.Llave.Count() != llaves) return false;
        if (protagonista.Inventario.LlaveMaestra.Count() != llaveMaestra) return false;
        if (protagonista.Inventario.PocionVida.Count() != pocionVida) return false;
        if (protagonista.Inventario.PocionLava.Count() != pocionLava) return false;
        if (protagonista.Inventario.Monedas != monedas) return false;
        if (protagonista.Inventario.Armadura != armadura) return false;
        //if (protagonista.Inventario.Daga.Count() != daga) return false;
        //if (protagonista.Inventario.Espada.Count() != espada) return false;

        return true;
    }

    void SetFuerza(int num)
    {
        fieldFUE.value = num;
    }
    void SetIntel(int num)
    {
        fieldINT.value = num;
    }
    void SetCarisma(int num)
    {
        fieldCAR.value = num;
    }
    void SetCA(int num)
    {
        fieldCA.value = num;
    }
    void SetConstitucion(int num)
    {
        fieldLIFE.value = num;
        ActualizarCorazon(num);
    }

    void ActualizarCorazon(int vidaActual)
    {
        float porcentaje = Mathf.Clamp01((float)vidaActual / maxLife);
        heartFill.style.height = new StyleLength(
            new Length(porcentaje * 100f, LengthUnit.Percent)
        );
    }

    public void MostrarNotificacion(Sprite icono)
    {
        Debug.Log("Mostrar notificacion");

        if (notifCoroutine != null)
        {
            StopCoroutine(notifCoroutine);
        }

        notifIcon.style.backgroundImage = new StyleBackground(icono);
        itemNotification.style.display = DisplayStyle.Flex;

        notifCoroutine = StartCoroutine(OcultarNotificacion(2.5f));
    }
    IEnumerator OcultarNotificacion(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        itemNotification.style.display = DisplayStyle.None;
        notifCoroutine = null;
        Debug.Log("No mostrar notificacion");

    }

    void SetInventario()
    {
        // Null check antes de acceder a las listas
        if (protagonista == null || protagonista.Inventario == null) return;
        if (protagonista.Inventario.Llave == null) return;
        if (protagonista.Inventario.LlaveMaestra == null) return;
        if (protagonista.Inventario.PocionVida == null) return;
        if (protagonista.Inventario.PocionLava == null) return;
        if (protagonista.Inventario.Monedas == 0) return;
        //if (protagonista.Inventario.Daga == null) return;
        //if (protagonista.Inventario.Espada == null) return;

        llaves = protagonista.Inventario.Llave.Count();
        llaveMaestra = protagonista.Inventario.LlaveMaestra.Count();
        pocionVida = protagonista.Inventario.PocionVida.Count();
        pocionLava = protagonista.Inventario.PocionLava.Count();
        //daga = protagonista.Inventario.Daga.Count();
        //espada = protagonista.Inventario.Espada.Count();
        monedas = protagonista.Inventario.Monedas;

        SetSlot(0, llaves > 0 ? iconoLlave : null, llaves);
        SetSlot(1, llaveMaestra > 0 ? iconoLlaveMaestra : null, llaveMaestra);
        SetSlot(2, pocionVida > 0 ? iconoPocionVida : null, pocionVida);
        //SetSlot(3, daga > 0 ? iconoDaga : null, daga);
        //SetSlot(4, espada > 0 ? iconoEspada : null, espada);
        //SetSlot(5, pocionLava > 0 ? iconoPocionLava : null, pocionLava);
        SetSlot(3, pocionLava > 0 ? iconoPocionLava : null, pocionLava);
        SetSlot(4, monedas > 0 ? iconoMonedas : null, monedas);


        SetArmadura(); //al abrir boton de armadura
    }

    void SetSlot(int index, Sprite icono, int cantidad)
    {
        var slotIcon = root.Q<VisualElement>($"slot-{index}-icon");
        var slotBadge = root.Q<Label>($"slot-{index}-badge");
        var slot = root.Q<VisualElement>($"slot-{index}");

        if (icono != null)
        {
            //si antes estaba vacio
            bool esNuevo = !slot.ClassListContains("inv-slot-active");
            slotIcon.style.backgroundImage = new StyleBackground(icono);
            slot.AddToClassList("inv-slot--active");
            //no activar, no para de apareixer sempre, ns el seu obj original per aixo el deixo

            if (esNuevo)
            {
                //Debug.Log(icono);
                MostrarNotificacion(icono);
            }

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
        if (cantidad <= 0 && index != 5)
            root.Q<VisualElement>($"btn_slot_{index}").SetEnabled(false);
    }
    void SetArmadura()
    {
        // Null check antes de acceder a las listas
        if (protagonista.Inventario.Armadura == null) return;
        if (protagonista.Inventario.CA_cuero == 0) return;
        if (protagonista.Inventario.CA_malla == 0) return;

        armadura = protagonista.Inventario.Armadura;
        cuero = protagonista.Inventario.CA_cuero;
        malla = protagonista.Inventario.CA_malla;

        if (armadura == "cuero")
        {
            protagonista.stats.values[4].value = 14;

            SetSlot(5, armadura != null ? iconoArmaduraCuero : null, 1);

        }
        else if (armadura == "malla")
        {
            protagonista.stats.values[4].value = 16;

            SetSlot(5, armadura != null ? iconoArmaduraMalla : null, 1);

        }
        else
        {
            protagonista.stats.values[4].value = 12;
            SetSlot(5, armadura != null ? iconoVacio : null, 0);

        }

        SetSlot(51, cuero > 0 ? iconoArmaduraCuero : null, cuero);

        SetSlot(52, malla > 0 ? iconoArmaduraMalla : null, malla);

    }
}
