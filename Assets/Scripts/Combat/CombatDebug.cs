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
    
    [Header("Possibles enemigos")]
    [SerializeField] Parameters cubo;

    CombatManager manager;

    private void Awake()
    {
        manager = GetComponent<CombatManager>();
        string nameEnemy = preload.nameOpponent();
        ElegirEnemigo(nameEnemy);
        manager.StartBattle(playerData, enemyData);   
    }

    private void Start()
    {

    }
    private void ElegirEnemigo(string NameEnemy)
    {
        //Aqui se tiene que elegir enemigo y enviar los parameters para que el
        //combate se realize con el oponente correcto
        enemyData = cubo;
        Debug.Log("NAME ENEMY="+NameEnemy);
        if (NameEnemy == "Cubo")
        {
            
            Debug.Log(enemyData);
        }
        else
        {
            Debug.Log("¿Quien es este enemigo?");
        }
    }
    public Parameters ReturnEnemy()
    {
        return enemyData;
    }
}
