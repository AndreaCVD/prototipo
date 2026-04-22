using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Menu_Command : MonoBehaviour
{
    //Canvas
    [Header("Canvas")]
    [SerializeField] CanvasGroup canvas_acciones;
    [SerializeField] CanvasGroup canva_Arma;
    [SerializeField] CanvasGroup canvas_enemy;
    [Header("Extraer info enemigo")]
    private CombatDebug info_enemy;
    private Parameters enemyData;

    [Header("Textos status")]
    [SerializeField] TMP_Text text_name;
    [SerializeField] TMP_Text text_fuerza;
    [SerializeField] TMP_Text text_intel;
    [SerializeField] TMP_Text text_cari;
    [SerializeField] TMP_Text text_const;

    private string h = "enemyData.stats.Get(PersonajesStats.Constitucion).ToString()";

    private void Start()
    {
        info_enemy = GetComponent<CombatDebug>();
        //canvas_acciones = GetComponent<CanvasGroup>();
        //Seteamos  opacidad
        //opacidad(0f);

        //iniciamos las stats del enemigo
        //Encontrar el script y las datas
        enemyData = info_enemy.ReturnEnemy();
        int f = enemyData.stats.Get(PersonajesStats.Fuerza);
        int i = enemyData.stats.Get(PersonajesStats.Inteligencia);
        int c = enemyData.stats.Get(PersonajesStats.Carisma);
 
        SetFuerza();
        SetIntel();
        SetCarisma();
        text_name.text = enemyData.namePers;
        //if (f > 0) //positivo
        //{
        //    SetFuerza("+");
        //}
        //else { SetFuerza("-"); } //negativo
        //if (i > 0) //positivo
        //{
        //    SetIntel("+");
        //}
        //else { SetIntel("-"); } //negativo     
        //if (c > 0) //positivo
        //{
        //    SetCarisma("+");
        //}
        //else { SetCarisma("-"); } //negativo
    }
    void Update()
    {
        //actualizar la constitucion del enemigo
        if (text_const.text != h)
        {
            SetConstitucion();
        }
    }
    //Aqui se reciben las nuevas opacidades
    public void opacidad(float nueva_opacidad)
    {
        // 0 == no se ve
        // 1 == se ve
        canvas_acciones.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
    void SetFuerza(/*string text*/)
    {
        text_fuerza.text = enemyData.stats.Get(PersonajesStats.Fuerza).ToString();
        //inteligencia.text = protagonista.stats.Get(PersonajesStats.Inteligencia).ToString();
        //constitucion.text = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();

    }
    void SetIntel(/*string text*/)
    {
        text_intel.text = enemyData.stats.Get(PersonajesStats.Inteligencia).ToString();

    }
    void SetCarisma(/*string text*/)
    {
        text_cari.text = enemyData.stats.Get(PersonajesStats.Carisma).ToString();
    }
    void SetConstitucion(/*string text*/)
    {
        text_const.text = enemyData.stats.Get(PersonajesStats.Constitucion).ToString();
        //h = enemyData.stats.Get(PersonajesStats.Constitucion).ToString();
    }


}