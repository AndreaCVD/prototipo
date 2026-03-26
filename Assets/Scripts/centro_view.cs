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


    void Start()
    {
        //Seteamos los textos
        //fuerza.text = "Fuerza = " + player.strengh();
        //inteligencia.text = "Inteligencia = " + player.intel();
        //carisma.text = "Carisma = " + player.charm();

        //Seteamos valores
        fuerza.text = "Fuerza = " + protagonista.stats.Get(PersonajesStats.Fuerza);
        inteligencia.text = "Inteligencia = " + protagonista.stats.Get(PersonajesStats.Inteligencia);
        carisma.text = "Carisma = " + protagonista.stats.Get(PersonajesStats.Carisma);

        //Seteamos el canva
        grup = GetComponent<CanvasGroup>();

    }

    //Aqui se reciben las nuevas opacidades
    //public void opacidad(float nueva_opacidad)
    //{
    //    grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    //}

}
