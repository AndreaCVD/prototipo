using System;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
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

    //UI
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    private IntegerField enemyConst;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        //enemyConst = root.Q("int_enemy_life").Q<IntegerField>();
        enemyConst = root.Q("int_life").Q<IntegerField>();
    }

    private void Start()
    {
        //iniciamos las stats del enemigo
        //encontrar el script y las datas
        info_enemy = GetComponent<CombatDebug>();
        enemyData = info_enemy.ReturnEnemy();

        enemyData = info_enemy.ReturnEnemy();
        //int f = enemyData.stats.Get(PersonajesStats.Fuerza);
        //int i = enemyData.stats.Get(PersonajesStats.Inteligencia);
        //int c = enemyData.stats.Get(PersonajesStats.Carisma);
 
        //SetFuerza();
        //SetIntel();
        //SetCarisma();
        //text_name.text = enemyData.namePers;

    }
    void Update()
    {

        int aux = enemyData.stats.Get(PersonajesStats.Constitucion);
        if (enemyConst.value != aux)
        {
            SetConstitucion(aux);
        }

    }

    //Aqui se reciben las nuevas opacidades
    public void opacidad(float nueva_opacidad)
    {
        // 0 == no se ve
        // 1 == se ve
        canvas_acciones.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }

    //void SetFuerza(/*string text*/)
    //{
    //    text_fuerza.text = enemyData.stats.Get(PersonajesStats.Fuerza).ToString();
    //    //inteligencia.text = protagonista.stats.Get(PersonajesStats.Inteligencia).ToString();
    //    //constitucion.text = protagonista.stats.Get(PersonajesStats.Constitucion).ToString();

    //}
    //void SetIntel(/*string text*/)
    //{
    //    text_intel.text = enemyData.stats.Get(PersonajesStats.Inteligencia).ToString();

    //}
    //void SetCarisma(/*string text*/)
    //{
    //    text_cari.text = enemyData.stats.Get(PersonajesStats.Carisma).ToString();
    //}

    void SetConstitucion(int num)
    {

        enemyConst.value = num;

        //text_const.text = enemyData.stats.Get(PersonajesStats.Constitucion).ToString();
    }


}