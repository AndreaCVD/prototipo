using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField] TurnRoundManager turnRoundManager;
    [SerializeField] CombatMonster opponent;
    
    //le llega la accion, mira la variable de turn,
    // y lo envia a combat monster
    public void Fuerza()
    {
        turnRoundManager.current.Fuerza(opponent);
    }
}
