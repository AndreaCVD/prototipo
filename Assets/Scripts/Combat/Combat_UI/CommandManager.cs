using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField] TurnRoundManager turnRoundManager;
    //[SerializeField] CombatMonster opponent;
    
    //le llega la accion, mira la variable de turn,
    // y lo envia a combat monster
    public void Fuerza()
    {
        //current es prota
        turnRoundManager.current.Fuerza(turnRoundManager.target);

        //Debug.Log("Current is = " + turnRoundManager.current);
        //Debug.Log("Target is = " + turnRoundManager.target);

        turnRoundManager.ChangeTurn();
    }
    public void Inteligencia()
    {
        //current es prota
        turnRoundManager.current.Inteligencia(turnRoundManager.target);

        //Debug.Log("Current is = " + turnRoundManager.current);
        //Debug.Log("Target is = " + turnRoundManager.target);

        turnRoundManager.ChangeTurn();
    }
    public void Carisma()
    {
        //current es prota
        turnRoundManager.current.Carisma(turnRoundManager.target);

        //Debug.Log("Current is = " + turnRoundManager.current);
        //Debug.Log("Target is = " + turnRoundManager.target);

        turnRoundManager.ChangeTurn();
    }
}
