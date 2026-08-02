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
    public int Armadura(int stat, int num_dado)
    {
        // ? = Nat 1
        // 0 = Nat 20
        // 2 = Tirada normal, AC superada
        // 3 = Tirada normal, AC NO superada

        Debug.Log("Tirar dado ");
        int aux = lanzarDado(20, 1);
        int AC_superada = turnRoundManager.current.Armadura(turnRoundManager.target, aux, stat);
        if (AC_superada == 3)
        {
            ActualizarHP();

            NextTurn();
            return 3;            
        }
        else
        {
            Debug.Log(AC_superada);
            return AC_superada;
        }
    }

    public void Fuerza(int dado, int times)
    {
        //RollDice(int maxValue, /*num veces a tirar dado*/)

        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = lanzarDado(dado, times);

        //Acción
        turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        //turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();
    }

    public void Inteligencia(int dado)
    {
        int aux = lanzarDado(dado, 1);

        //Acción
        turnRoundManager.current.Inteligencia(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();

    }
    public void Carisma(int dado)
    {
        int aux = lanzarDado(20, 1);

        //Acción
        turnRoundManager.current.Carisma(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();
    }
    private int lanzarDado(int caras, int tiradas)
    {
        int a = diceRoller.RollDice(caras, tiradas);
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
