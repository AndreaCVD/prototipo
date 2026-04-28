using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//Enviarle al combat manager los personajes que se peleen
//para hacer test de combate
public class CombatDebug : MonoBehaviour
{

    [Header("Para el combate")]
    [SerializeField] Preload preload;
    //Prota
    [SerializeField] Parameters playerData;
    //Oponente combate
    private Parameters enemyData;
    
    [Header("Possibles enemigos a combatir")]
    //[SerializeField] List<Parameters> Enemigos = new List<Parameters>();

    [SerializeField] Parameters cubo;
    [SerializeField] Parameters caballero;

    CombatManager manager;

    private void Awake()
    {
        manager = GetComponent<CombatManager>();


    }

    private void Start()
    {
        ElegirEnemigo();

        manager.StartBattle(playerData, enemyData);   
    }
    private void ElegirEnemigo()
    {
        string nameEnemy = preload.nameOpponent();

        //Aqui se tiene que elegir enemigo y enviar los parameters para que el
        //combate se realize con el oponente correcto

        
        Debug.Log("NAME ENEMY="+ nameEnemy);

        switch (nameEnemy)
        {
            case "nada":
                enemyData = cubo;
                break;
            case "Caballero":
                enemyData = caballero;
                break;
            default:
                Debug.Log("No se ha identificado al enemigo");
                break;
        }
        //if (NameEnemy == "Cubo")
        //{
            
        //    Debug.Log(enemyData);
        //}
        //else
        //{
        //    Debug.Log("¿Quien es este enemigo?");
        //}
    }
    public Parameters ReturnEnemy()
    {
        return enemyData;
    }
}
