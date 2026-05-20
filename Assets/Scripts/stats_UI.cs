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

    private List<string> aux = new List<string>();
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
        aux = new List<string>(protagonista.Inventario);
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
            SetInventario(/*protagonista.Inventario*/);
        }
    }
    bool areListEqual()
    {
        var x = new List<string>(protagonista.Inventario);
        //Si  no son iguales de largo entonces estaran mal
        Debug.Log(aux.Count);
        Debug.Log(x.Count);
        if (aux.Count != x.Count) return false;
        
        //Si son del mismo tamaño
        for (int i = 0; i < x.Count; i++)
        {
            if (x[i] != aux[i])
            {
                Debug.Log("The " + i.ToString() + "th character is different.");
                return false;
            }
        }

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
    void SetInventario(/*List<string> listaX*/)
    {
        List<string> inv = new List<string>(protagonista.Inventario);
        Debug.Log(inv);
        //Primero destruimos todos los hijos
        foreach (Transform child in contenedor)
        {
            Destroy(child.gameObject);
        }
        //Volvemos a llenar todo
        foreach (string dato in inv)
        {
            // 1. Clonar el prefab
            GameObject nuevoElemento = Instantiate(prefabElemento, contenedor);

            // 2. Asignar el texto
            Text textoComponente = nuevoElemento.GetComponent<Text>();
            if (textoComponente != null)
            {
                textoComponente.text = dato;
            }
        }
    }

}
