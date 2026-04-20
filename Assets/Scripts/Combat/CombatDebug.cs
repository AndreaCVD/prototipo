using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//Enviarle al combat manager los personajes que se peleen
//para hacer test de combate
public class CombatDebug : MonoBehaviour
{

    [Header("Para el combate")]
    //Prota
    [SerializeField] Parameters playerData;
    //Oponente combate
    [SerializeField] Parameters enemyData;
    
    [Header("Possibles enemigos")]
    [SerializeField] Parameters cubo;

    CombatManager manager;

    private void Awake()
    {
        manager = GetComponent<CombatManager>();

    }

    private void Start()
    {
        manager.StartBattle(playerData, enemyData); 
    }

    public Parameters ReturnEnemy()
    {
        return enemyData;
    }
}
