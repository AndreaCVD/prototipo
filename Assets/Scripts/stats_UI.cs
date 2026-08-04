using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; // Imprescindible para UI Toolkit
using System.Linq;
using System.Collections; //Comparacion de Listas

public class stats_UI : MonoBehaviour
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
    [SerializeField] Sprite iconoMonedas;

    //ref del UI
    private VisualElement root;
    private IntegerField fieldFUE, fieldINT, fieldCAR, fieldLIFE;
    private VisualElement heartFill;
    private int maxLife;

    //inv en el UI
    private VisualElement inventoryGrid;
    private Button btnInventory;
    private VisualElement itemNotification;
    private VisualElement notifIcon;
    private Coroutine notifCoroutine;

    //contadores inventory
    private int llaves;
    private int llaveMaestra;
    //private int daga;
    //private int espada;
    private int pocionVida;
    private int pocionLava;
    private int monedas;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        //stats
        fieldFUE = root.Q("FUE").Q<IntegerField>();
        fieldINT = root.Q("INT").Q<IntegerField>();
        fieldCAR = root.Q("CAR").Q<IntegerField>();
        fieldLIFE = root.Q("int_life").Q<IntegerField>();
        heartFill = root.Q<VisualElement>("heart-fill");

        //inventary
        inventoryGrid = root.Q<VisualElement>("inventory-grid");
        btnInventory = root.Q<Button>("btn-inventory");
        btnInventory.clicked += ToggleInventary;

        inventoryGrid.style.display = DisplayStyle.None;

        itemNotification = root.Q<VisualElement>("item-notification");
        notifIcon = root.Q<VisualElement>("notif-icon");
    }
    private void OnDisable()
    {
        btnInventory.clicked -= ToggleInventary;
    }

    void Start()
    {
        llaves = 0;
        llaveMaestra = 0;
        //espada = 0;
        //daga = 0;
        pocionVida = 0;
        pocionLava = 0;
        monedas = 0;

        //Seteamos valores, int -> string
        int f = protagonista.stats.Get(PersonajesStats.Fuerza);
        int i = protagonista.stats.Get(PersonajesStats.Inteligencia);
        int c = protagonista.stats.Get(PersonajesStats.Carisma);
        //h = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();
        //int h = protagonista.stats.Get(PersonajesStats.Constitucion);

        maxLife = protagonista.stats.Get(PersonajesStats.Max_Vida);

        SetFuerza(f);
        SetIntel(i);
        SetCarisma(c);
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

        //abrir/cerrar con la tecla I
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventary();
        }
    }

    void ToggleInventary()
    {
        bool isDisplayed = inventoryGrid.style.display == DisplayStyle.Flex;
        inventoryGrid.style.display = isDisplayed ? DisplayStyle.None : DisplayStyle.Flex;
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
        if (protagonista.Inventario.Monedas == null) return false;
        //if (protagonista.Inventario.Espada == null) return false;

        if (protagonista.Inventario.Llave.Count() != llaves) return false;
        if (protagonista.Inventario.LlaveMaestra.Count() != llaveMaestra) return false;
        if (protagonista.Inventario.PocionVida.Count() != pocionVida) return false;
        if (protagonista.Inventario.PocionLava.Count() != pocionLava) return false;
        if (protagonista.Inventario.Monedas.Count() != monedas) return false;
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
        //Debug.Log("Mostrar notificacion");
        /*
        if (notifCoroutine != null)
        {
            StopCoroutine(notifCoroutine);
        }

        notifIcon.style.backgroundImage = new StyleBackground(icono);
        itemNotification.style.display = DisplayStyle.Flex;

        notifCoroutine = StartCoroutine(OcultarNotificacion(2.5f));*/
    }

    IEnumerator OcultarNotificacion(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        itemNotification.style.display = DisplayStyle.None;
        notifCoroutine = null;
    }

    void SetInventario()
    {
        // Null check antes de acceder a las listas
        if (protagonista == null || protagonista.Inventario == null) return;
        if (protagonista.Inventario.Llave == null) return;
        if (protagonista.Inventario.LlaveMaestra == null) return;
        if (protagonista.Inventario.PocionVida == null) return;
        if (protagonista.Inventario.PocionLava == null) return;
        if (protagonista.Inventario.Monedas == null) return;
        //if (protagonista.Inventario.Daga == null) return;
        //if (protagonista.Inventario.Espada == null) return;

        llaves = protagonista.Inventario.Llave.Count();
        llaveMaestra = protagonista.Inventario.LlaveMaestra.Count();
        pocionVida = protagonista.Inventario.PocionVida.Count();
        pocionLava = protagonista.Inventario.PocionLava.Count();
        //daga = protagonista.Inventario.Daga.Count();
        //espada = protagonista.Inventario.Espada.Count();
        monedas = protagonista.Inventario.Monedas.Count;

        SetSlot(0, llaves > 0 ? iconoLlave : null, llaves);
        SetSlot(1, llaveMaestra > 0 ? iconoLlaveMaestra : null, llaveMaestra);
        SetSlot(2, pocionVida > 0 ? iconoPocionVida : null, pocionVida);
        //SetSlot(3, daga > 0 ? iconoDaga : null, daga);
        //SetSlot(4, espada > 0 ? iconoEspada : null, espada);
        //SetSlot(5, pocionLava > 0 ? iconoPocionLava : null, pocionLava);
        SetSlot(3, pocionLava > 0 ? iconoPocionLava : null, pocionLava);
        SetSlot(4, monedas > 0 ? iconoMonedas : null, monedas);
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
    }
}
