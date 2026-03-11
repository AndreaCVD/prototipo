using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//El objetivo de este script es:
// AJUSTAR OPACIDAD depeniendo si entra o sale del collider del obj
public class centro_view : MonoBehaviour
{
    [Header("Stats")]
    public personaje player;
    [Header("Canvas")]
    public CanvasGroup grup;
    //Textos
    [SerializeField] TMP_Text fuerza;
    [SerializeField] TMP_Text inteligencia;
    [SerializeField] TMP_Text carisma;


    void Start()
    {
        //Seteamos los textos
        fuerza.text = "Fuerza = " + player.strengh();
        inteligencia.text = "Inteligencia = " + player.intel();
        carisma.text = "Carisma = " + player.charm();

        //Seteamos el canva
        grup = GetComponent<CanvasGroup>();

    }

        //Aqui se reciben las nuevas opacidades
    //public void opacidad(float nueva_opacidad)
    //{
    //    grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    //}

}
