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

    public bool gameOver, enemigo_inmovilizado, enemigo_inLove;
    private int turnos_inmovil = 1;

    //[SerializeField] CombatMonster opponent;

    //current = al que le toque el turno

    //le llega la accion, mira la variable de turn,
    // y lo envia a combat monster
    private void Awake()
    {
        menuCommand = GetComponent<Menu_Command>();
        combatDebug = GetComponent<CombatDebug>();


    }
    void Start()
    {
        enemigo_inLove = turnRoundManager.target.InLove();
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
            return AC_superada;
        }
    }
    public void Huir()
    {
        //Enemigo hace d4 y se cierra el combate
        if (!enemigo_inLove)
        {
            turnRoundManager.ChangeTurn();
            turnRoundManager.AtaqueOportunidad();
        }
        else
        {
            //Si esta enamorado no te persigue
            turnRoundManager.current.SalirCombate();

        }
    }
    public void SimpleAttack(int dado, int times, bool acabarCombate)
    {
        int aux = lanzarDado(dado, times);
        turnRoundManager.current.SimpleAttack(turnRoundManager.target, aux, acabarCombate);

    }
    public void AutoHerirse(int dado, int times)
    {
        int aux = lanzarDado(dado, times);

        turnRoundManager.current.ataque_propio(turnRoundManager.current, aux);
        ActualizarHP();

        NextTurn();
    }
    public void Fuerza(int dado, int times)
    {
        //RollDice(int maxValue, /*num veces a tirar dado*/)

        //Hay que llamar al DiceRoller para ver si superamos el AC
        int aux = lanzarDado(dado, times);

        //Acci�n
        turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        //turnRoundManager.current.Fuerza(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();
    }

    public void Inteligencia(int dado)
    {
        int aux = lanzarDado(dado, 1);

        //Acci�n
        turnRoundManager.current.Inteligencia(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();

    }
    public void Modificar_CA(int valor)
    {
        turnRoundManager.current.modificar_CA(valor);
        ActualizarHP();

        NextTurn();
    }
    public bool Return_inLove()
    {
        enemigo_inLove = turnRoundManager.target.InLove();
        if (enemigo_inLove)
            return true;
        else
            enemigo_inLove = false;
            return false;
    }
    public void enemigoEnamorado()
    {
        enemigo_inLove = true;
        //Enemigo no tiene que atacar mas
        turnRoundManager.target.Enamorado();

    }
    public void EnemigoInmovilizado(bool inmov)
    {
        enemigo_inmovilizado = inmov;
    }
    public void Carisma(int dado)
    {
        int aux = lanzarDado(20, 1);

        //Acci�n
        turnRoundManager.current.Carisma(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();
    }
    private int lanzarDado(int caras, int tiradas)
    {
        int a = diceRoller.RollDice(caras, tiradas);
        return a; 
    }
    private int lanzarDado(int caras_1, int tiradas_1, int caras_2, int tiradas_2)
    {
        int a = diceRoller.RollDice(caras_1, tiradas_1, caras_2, tiradas_2);
        return a; 
    }
    public void NextTurn()
    {
        if (enemigo_inmovilizado)
        {
            //No cambiamos el turno
            //Activar animacion 
            turnos_inmovil--;
            Debug.Log("Enemigo Inmovilizado. Le quedan = " + turnos_inmovil);
            if (turnos_inmovil == 0)
            {
                Debug.Log("Ya no esta inmovilizado");
                enemigo_inmovilizado = false;
            }
        }
        else if (enemigo_inLove)
        {
            Debug.Log("Enemigo enamorado, no te ataca");
        }
        else
        {
            //Cambiamos turno, y vemos si es el turno del enemigo
            turnRoundManager.ChangeTurn();
            turnRoundManager.EnemyTurn();
        }
    }

    private void ActualizarHP()
    {
        int playerHp = combatDebug.ReturnPlayer().stats.Get(PersonajesStats.Constitucion);
        int enemyHp = combatDebug.ReturnEnemy().stats.Get(PersonajesStats.Constitucion);
        menuCommand.SetPlayerHp(playerHp);
        menuCommand.SetEnemyHp(enemyHp);
    }
}
