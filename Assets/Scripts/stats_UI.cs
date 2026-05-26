using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements; // Imprescindible para UI Toolkit
using System.Linq; //Comparacion de Listas

public class stats_UI : MonoBehaviour
{
    [Header("Ficha personaje")]
    [SerializeField] Parameters protagonista;

    //ref del UI
    private VisualElement root;
    private IntegerField fieldFUE, fieldINT, fieldCAR, fieldLIFE;
    public bool canvas_open;
    public CanvasGroup canvas_inventario;

    private VisualElement heartFill;
    private int maxLife;

    [Header("Inventario")]
    [SerializeField] TMP_Text llave_text;
    [SerializeField] TMP_Text llaveMaestra_text;
    [SerializeField] TMP_Text daga_text;
    [SerializeField] TMP_Text espada_text;
    [SerializeField] TMP_Text pocionVida_text;

    // [SerializeField] TMP_Text texto_inventario;
    //[SerializeField] GameObject prefabElemento; // Arrastra el Text prefab aquí en el inspector
    //public Transform contenedor; // Arrastra el "ContenedorLista" del Canvas aquí
    
    private int llaves;
    private int llaveMaestra;
    private int daga;
    private int espada;
    private int pocionVida;
    // var listaX;
    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        fieldFUE = root.Q("FUE").Q<IntegerField>();
        fieldINT = root.Q("INT").Q<IntegerField>();
        fieldCAR = root.Q("CAR").Q<IntegerField>();

        fieldLIFE = root.Q("int_life").Q<IntegerField>();
        heartFill = root.Q<VisualElement>("heart-fill");
    }

    void Start()
    {
        llaves = 0;
        llaveMaestra = 0;
        espada = 0;
        daga = 0;
        pocionVida = 0;

        canvas_open = true;
        //abrirInventario();
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
        //aux = new List<int>(protagonista.Inventario);
    }

    void Update()
    {
        int aux = protagonista.stats.Get(PersonajesStats.Constitucion);
        if (fieldLIFE.value != aux)
        {
            SetConstitucion(aux);
        }

        bool x = areListEqual();
        if (!x)
        {
            SetInventario();
        }
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

        llave_text.text = "Llaves = " + llaves.ToString();
        llaveMaestra_text.text = "Llave Maestra = " + llaveMaestra.ToString();
        pocionVida_text.text = "Pocion = " + pocionVida.ToString();
        daga_text.text = "Daga = " + daga.ToString();
        espada_text.text = "Espada = " + espada.ToString();
    }
    public void abrirInventario()
    {
        //Debug.Log("se esta cerrando?");
        //if (!canvas_open)
        //{
        //    canvas_open = true;
        //    canvas_inventario.alpha = Mathf.Lerp(0f, 1f, 5f);
        //}
        //else
        //{
        //    canvas_open = false;
        //    canvas_inventario.alpha = Mathf.Lerp(0f, 0f, 5f);
        //}

    }
}
