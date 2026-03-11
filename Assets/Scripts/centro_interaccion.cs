using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//El objetivo de este script es:
// AJUSTAR OPACIDAD depeniendo si entra o sale del collider del obj

public class centro_interaccion : MonoBehaviour
{
    //Canvas
    [Header("Canva")]
    public CanvasGroup grup;
    //Textos
    [Header("Textos Canva")]
    [SerializeField] TMP_Text op_fuerza;
    [SerializeField] TMP_Text op_intel;
    [SerializeField] TMP_Text op_cari;

    void Start()
    {
        //Seteamos los textos
        op_fuerza.text = "Fuerza";
        op_intel.text = "Inteligencia";
        op_cari.text = "Carismaa";
   
        //Seteamos el canva
        grup = GetComponent<CanvasGroup>();

        //Seteamos  opacidad
        opacidad(0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void textos (string fuerza, string intel, string carisma)
    {
        op_fuerza.text = fuerza;
        op_intel.text = intel;
        op_cari.text = carisma;
    }
    //Aqui se reciben las nuevas opacidades
    public void opacidad(float nueva_opacidad)
    {
        grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
}
