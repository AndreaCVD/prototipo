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

    //current = al que le toque el turno

    //le llega la accion, mira la variable de turn,
    // y lo envia a combat monster
    public void Llave()
    {
        //turnRoundManager.current.Fuerza(turnRoundManager.target, aux);

    }
    public void LlaveMaestra()
    {

    }
    public void Daga()
    {
        //Subir ataque por 1 turno
    }
    public void Espada()
    {
        //Subir ataque

    }
    public void PocionVida()
    {
        //Subir vida
        turnRoundManager.current.cambiarVida(10);
    }

    public void Fuerza()
    {
        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = lanzarDado(20);

        //Acción
        turnRoundManager.current.Fuerza(turnRoundManager.target, aux);

        NextTurn();
    }
    public void Inteligencia()
    {
        int aux = lanzarDado(20);

        //Acción
        turnRoundManager.current.Inteligencia(turnRoundManager.target, aux);


        NextTurn();

    }
    public void Carisma()
    {
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
