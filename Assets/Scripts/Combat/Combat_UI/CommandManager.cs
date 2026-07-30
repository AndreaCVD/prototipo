using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandManager : MonoBehaviour
{

    [SerializeField] private Menu_Command menuCommand;
    [SerializeField] TurnRoundManager turnRoundManager;
    [SerializeField] CombatDebug combatDebug;
    [SerializeField] Dice diceRoller;

    bool gameOver;
    //[SerializeField] CombatMonster opponent;

    //current = al que le toque el turno

    //le llega la accion, mira la variable de turn,
    // y lo envia a combat monster
    private void Awake()
    {
        menuCommand = GetComponent<Menu_Command>();
        combatDebug = GetComponent<CombatDebug>();
    }

    //Items
    public void PocionVida()
    {
        //Subir vida
        turnRoundManager.current.cambiarVida(10);
        ActualizarHP();
    }
    //Ver si supera la armadura del contrincante
    public bool Armadura(int stat, int num_dado)
    {
        Debug.Log("Tirar dado ");
        int aux = lanzarDado(20);
        bool AC_superada = turnRoundManager.current.Armadura(turnRoundManager.target, aux, stat);
        if (AC_superada)
        {
            Debug.Log("Has llegado al AC del enemigo");
            
            return true;
        }
        else
        {
            Debug.Log("No has llegado al AC del enemigo");
            ActualizarHP();

            NextTurn();
            return false;
        }
    }

    public void Fuerza(int dado)
    {
        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = lanzarDado(dado);

        //Acción
        turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        //turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();
    }
    public void Inteligencia(int dado)
    {
        int aux = lanzarDado(dado);

        //Acción
        turnRoundManager.current.Inteligencia(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();

    }
    public void Carisma(int dado)
    {
        int aux = lanzarDado(20);

        //Acción
        turnRoundManager.current.Carisma(turnRoundManager.target, aux);
        ActualizarHP();

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

    private void ActualizarHP()
    {
        int playerHp = combatDebug.ReturnPlayer().stats.Get(PersonajesStats.Constitucion);
        int enemyHp = combatDebug.ReturnEnemy().stats.Get(PersonajesStats.Constitucion);
        menuCommand.SetPlayerHp(playerHp);
        menuCommand.SetEnemyHp(enemyHp);
    }
}
