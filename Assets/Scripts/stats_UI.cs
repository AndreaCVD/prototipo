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


    private VisualElement root;
    //ref del UI
    private IntegerField fieldFUE, fieldINT, fieldCAR, fieldLIFE;
    
   // [SerializeField] TMP_Text texto_inventario;
    [SerializeField] GameObject prefabElemento; // Arrastra el Text prefab aquí en el inspector
    public Transform contenedor; // Arrastra el "ContenedorLista" del Canvas aquí
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
    }

    void Start()
    {
        llaves = 0;
        llaveMaestra = 0;
        espada = 0;
        daga = 0;
        pocionVida = 0;

        //Seteamos valores, int -> string
        int f = protagonista.stats.Get(PersonajesStats.Fuerza);
        int i = protagonista.stats.Get(PersonajesStats.Inteligencia);
        int c = protagonista.stats.Get(PersonajesStats.Carisma);
        //h = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();
        //int h = protagonista.stats.Get(PersonajesStats.Constitucion);


            SetFuerza(f);

            SetIntel(i);

            SetCarisma(c);

        //SetInventario();
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
    }
    void SetInventario()
    {
        llaves = protagonista.Inventario.Llave.Count();
        llaveMaestra = protagonista.Inventario.LlaveMaestra.Count();
        pocionVida = protagonista.Inventario.PocionVida.Count();
        daga = protagonista.Inventario.Daga.Count();
        espada = protagonista.Inventario.Espada.Count();

    }

}
