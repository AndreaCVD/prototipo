using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//Enviarle al combat manager los personajes que se peleen
//para hacer test de combate
public class CombatDebug : MonoBehaviour
{
    [SerializeField] Parameters playerData;
    [SerializeField] Parameters enemyData;
    
    CombatManager manager;

    private void Awake()
    {
        manager = GetComponent<CombatManager>();
    }

    private void Start()
    {
        manager.StartBattle(playerData, enemyData); 
    }
}
