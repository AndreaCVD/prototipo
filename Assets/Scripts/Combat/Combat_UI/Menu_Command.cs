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

    [Header("Extraer info enemigo")]
    private CombatDebug info_enemy;
    private Parameters enemyData;
    private int enemyMaxHp;

    [Header("Extraer info prota")]
    private CombatDebug info_prota;
    private Parameters playerData;
    private int playerMaxHp;

    //UI
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    private VisualElement playerHpFill;
    private VisualElement enemyHpFill;

    private void OnEnable()
    {
        //var uiDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;

        playerHpFill = root.Q<VisualElement>("player-hp-fill");
        enemyHpFill = root.Q<VisualElement>("enemy-hp-fill");

    }

    private void Start()
    {
        //iniciamos las stats del enemigo
        //encontrar el script y las datas
        info_enemy = GetComponent<CombatDebug>();
        enemyData = info_enemy.ReturnPlayer();

        info_prota = GetComponent<CombatDebug>();
        playerData = info_prota.ReturnPlayer();

        // Inicializar barras al 100%
        SetPlayerHp(playerData.stats.Get(PersonajesStats.Constitucion));
        SetEnemyHp(enemyData.stats.Get(PersonajesStats.Constitucion));
        Debug.Log("Prota: " + playerMaxHp + " Enemy: " + playerMaxHp);

    }

    // Llamar desde el sistema de combate cuando el jugador recibe daño
    public void SetPlayerHp(int hpActual)
    {
        float porcentaje = Mathf.Clamp01((float)hpActual / playerMaxHp);
        playerHpFill.style.width = Length.Percent(porcentaje * 100f);
    }

    // Llamar desde el sistema de combate cuando el enemigo recibe daño
    public void SetEnemyHp(int hpActual)
    {
        float porcentaje = Mathf.Clamp01((float)hpActual / enemyMaxHp);
        enemyHpFill.style.width = Length.Percent(porcentaje * 100f);
    }

    public void opacidad(float nueva_opacidad)
    {
        canvas_acciones.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }


}