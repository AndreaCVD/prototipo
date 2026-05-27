using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UI = UnityEngine.UI;
using UnityEngine.UIElements; // Imprescindible para UI Toolkit
using System.Linq; //Comparacion de Listas

public class stats_UI : MonoBehaviour
{
    [Header("Ficha personaje")]
    [SerializeField] Parameters protagonista;

    //ref del UI
    private VisualElement root;
    private IntegerField fieldFUE, fieldINT, fieldCAR, fieldLIFE;
    private VisualElement heartFill;
    private int maxLife;

    //inv en el UI
    private VisualElement inventoryGrid;
    private Button btnInventory;

    //contadores inventory
    private int llaves;
    private int llaveMaestra;
    private int daga;
    private int espada;
    private int pocionVida;

    //lo del canva para quitar en el futuro
    [Header("Inventario")]
    [SerializeField] TMP_Text llave_text;
    [SerializeField] TMP_Text llaveMaestra_text;
    [SerializeField] TMP_Text daga_text;
    [SerializeField] TMP_Text espada_text;
    [SerializeField] TMP_Text pocionVida_text;
    public bool canvas_open;
    public CanvasGroup canvas_inventario;

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
    }
    private void OnDisable()
    {
        btnInventory.clicked -= ToggleInventary;
    }

    void Start()
    {
        llaves = 0;
        llaveMaestra = 0;
        espada = 0;
        daga = 0;
        pocionVida = 0;

        canvas_open = false;

        //Seteamos valores, int -> string
        int f = protagonista.stats.Get(PersonajesStats.Fuerza);
        int i = protagonista.stats.Get(PersonajesStats.Inteligencia);
        int c = protagonista.stats.Get(PersonajesStats.Carisma);
        //h = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();
        //int h = protagonista.stats.Get(PersonajesStats.Constitucion);

        maxLife = protagonista.stats.Get(PersonajesStats.Constitucion);

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
        //Si  no son iguales de largo entonces estaran mal 100%
        if (protagonista.Inventario.Llave.Count() != llaves) return false;
        if (protagonista.Inventario.LlaveMaestra.Count() != llaveMaestra) return false;
        if (protagonista.Inventario.PocionVida.Count() != pocionVida) return false;
        if (protagonista.Inventario.Daga.Count() != daga) return false;
        if (protagonista.Inventario.Espada.Count() != espada) return false;

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
    void SetInventario()
    {
        llaves = protagonista.Inventario.Llave.Count();
        llaveMaestra = protagonista.Inventario.LlaveMaestra.Count();
        pocionVida = protagonista.Inventario.PocionVida.Count();
        daga = protagonista.Inventario.Daga.Count();
        espada = protagonista.Inventario.Espada.Count();

        /*para eliminar
        llave_text.text = "Llaves = " + llaves.ToString();
        llaveMaestra_text.text = "Llave Maestra = " + llaveMaestra.ToString();
        pocionVida_text.text = "Pocion = " + pocionVida.ToString();
        daga_text.text = "Daga = " + daga.ToString();
        espada_text.text = "Espada = " + espada.ToString();
        */
    }
}
