using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField] TurnRoundManager turnRoundManager;
    [SerializeField] Dice diceRoller;
    bool gameOver;
    //[SerializeField] CombatMonster opponent;
    
    //le llega la accion, mira la variable de turn,
    // y lo envia a combat monster
    public void Fuerza()
    {
        //current = al que le toque el turno
        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = lanzarDado(20);

        //Acción
        turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        //Debug.Log("Accion completada");

        //Debug.Log("Current is = " + turnRoundManager.current);
        //Debug.Log("Target is = " + turnRoundManager.target);
        NextTurn();
    }
    public void Inteligencia()
    {
        //current = al que le toque el turno
        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = lanzarDado(20);

        //Acción
        turnRoundManager.current.Inteligencia(turnRoundManager.target, aux);


        NextTurn();

    }
    public void Carisma()
    {
        //current = al que le toque el turno
        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = lanzarDado(20);

        //Acción
        turnRoundManager.current.Carisma(turnRoundManager.target, aux);

        NextTurn();
    }
    private int lanzarDado(int caras)
    {
        int a = diceRoller.RollDice(caras);
        Debug.Log("El dado se ha lanzado");
        return a; 
    }
    private void NextTurn()
    {
        //Cambiamos turno, y vemos si es el turno del enemigo
        turnRoundManager.ChangeTurn();
        turnRoundManager.EnemyTurn();
    }
}
