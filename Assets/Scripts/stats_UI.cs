using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements; // Imprescindible para UI Toolkit

public class stats_UI : MonoBehaviour
{
    [Header("Ficha personaje")]
    public Parameters protagonista;

    private VisualElement root;
    //ref del UI
    private IntegerField fieldFUE, fieldINT, fieldCAR, fieldLIFE;

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

    }
    void Update()
    {
        int aux = protagonista.stats.Get(PersonajesStats.Constitucion);
        if (fieldLIFE.value != aux)
        {
            SetConstitucion(aux);
        }
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

    //Aqui se reciben las nuevas opacidades
    //public void opacidad(float nueva_opacidad)
    //{
    //    grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    //}

}
