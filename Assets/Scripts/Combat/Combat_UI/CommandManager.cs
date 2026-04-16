using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField] TurnRoundManager turnRoundManager;
    [SerializeField] Dice diceRoller;

    //[SerializeField] CombatMonster opponent;
    
    //le llega la accion, mira la variable de turn,
    // y lo envia a combat monster
    public void Fuerza()
    {
        //current = al que le toque el turno
        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = diceRoller.RollDice(20);
        Debug.Log("El dado se ha lanzado");
        turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        Debug.Log("Accion completada");

        //Debug.Log("Current is = " + turnRoundManager.current);
        //Debug.Log("Target is = " + turnRoundManager.target);

        turnRoundManager.ChangeTurn();
    }
    public void Inteligencia()
    {
        //current = al que le toque el turno
        turnRoundManager.current.Inteligencia(turnRoundManager.target);

        //Debug.Log("Current is = " + turnRoundManager.current);
        //Debug.Log("Target is = " + turnRoundManager.target);

        turnRoundManager.ChangeTurn();
    }
    public void Carisma()
    {
        //current = al que le toque el turno
        turnRoundManager.current.Carisma(turnRoundManager.target);

        //Debug.Log("Current is = " + turnRoundManager.current);
        //Debug.Log("Target is = " + turnRoundManager.target);

        turnRoundManager.ChangeTurn();
    }
}
