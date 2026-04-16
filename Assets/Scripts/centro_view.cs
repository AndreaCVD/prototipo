using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//El objetivo de este script es:
// AJUSTAR OPACIDAD depeniendo si entra o sale del collider del obj
public class centro_view : MonoBehaviour
{
    [Header("Ficha personaje")]
    public Parameters protagonista;
    //public personaje player;
    [Header("Canvas")]
    public CanvasGroup grup;
    //Textos
    [SerializeField] TMP_Text fuerza;
    [SerializeField] TMP_Text inteligencia;
    [SerializeField] TMP_Text carisma;
    [SerializeField] TMP_Text constitucion;


    void Start()
    {
        //Seteamos valores, int -> string
        int f = protagonista.stats.Get(PersonajesStats.Fuerza);
        int i = protagonista.stats.Get(PersonajesStats.Inteligencia);
        int c = protagonista.stats.Get(PersonajesStats.Carisma);
        int h = protagonista.stats.Get(PersonajesStats.Carisma);
        //int h = protagonista.stats.Get(PersonajesStats.Constitucion);

        if (f > 0) //positivo
        {
            SetFuerza("+");
            //fuerza.text = "+" + protagonista.stats.Get(PersonajesStats.Fuerza).ToString();
        }
        else { SetFuerza("-"); } //negativo
        if (i > 0) //positivo
        {
            SetIntel("+");
            //fuerza.text = "+" + protagonista.stats.Get(PersonajesStats.Fuerza).ToString();
        }
        else { SetIntel("-"); } //negativo     
        if (c > 0) //positivo
        {
            SetCarisma("+");
            //fuerza.text = "+" + protagonista.stats.Get(PersonajesStats.Fuerza).ToString();
        }
        else { SetCarisma("-"); } //negativo
        if (h > 0) //positivo
        {
            SetCarisma("+");
            //fuerza.text = "+" + protagonista.stats.Get(PersonajesStats.Fuerza).ToString();
        }
        else { SetConstitucion("-"); } //negativo
        constitucion.text = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();

        //Seteamos el canva
        grup = GetComponent<CanvasGroup>();

    }
    void SetFuerza(string text)
    {
        fuerza.text = text + protagonista.stats.Get(PersonajesStats.Fuerza).ToString();
        //inteligencia.text = protagonista.stats.Get(PersonajesStats.Inteligencia).ToString();
        //constitucion.text = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();

    }
    void SetIntel(string text)
    {
        inteligencia.text = text + protagonista.stats.Get(PersonajesStats.Inteligencia).ToString();

    }
    void SetCarisma(string text)
    {
        carisma.text = text + protagonista.stats.Get(PersonajesStats.Carisma).ToString();
    }
    void SetConstitucion(string text)
    {
        constitucion.text = text + protagonista.stats.Get(PersonajesStats.Constitucion).ToString();
    }

    //Aqui se reciben las nuevas opacidades
    //public void opacidad(float nueva_opacidad)
    //{
    //    grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    //}

}
