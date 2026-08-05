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

    public bool gameOver, enemigo_inmovilizado, enemigo_inLove, enfadado, asustado;
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
    // --- ATAQUES SIMPLES ---
    public void Fuerza(int dado, int times)
    {
        int aux = lanzarDado(dado, times);
        turnRoundManager.current.Fuerza(turnRoundManager.target, aux);

        ActualizarHP();

        NextTurn();
    }
    public void Carisma(int dado)
    {
        int aux = lanzarDado(20, 1);

        //Acci�n
        turnRoundManager.current.Carisma(turnRoundManager.target, aux);
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
    // --- MOVIMIENTOS ENEMIGO ---
    public void AtaqueEnfadado(int dado, int times)
    {
        int armadura = Armadura(0, 20);
        enfadado = false;
        if (armadura == 2) //supera armadura
        {
            Debug.Log("Ataque de enfado del enemigo ha funcionado");

            int aux = lanzarDado(dado, times);
            int aux_2 = lanzarDado(4, 1);
            turnRoundManager.current.Fuerza(turnRoundManager.target, aux + aux_2);

            ActualizarHP();
            NextTurn();
    }
        else if (armadura == 0) //CRITICO
        {
            Debug.Log("Ataque de enfado del enemigo es critico");

            int aux = lanzarDado(dado, times);
            int aux_2 = lanzarDado(4, 1);
            int aux_3 = lanzarDado(4, 1);
            turnRoundManager.current.Fuerza(turnRoundManager.target, aux + aux_2 +aux_3);

            ActualizarHP();
            NextTurn();
        }
        else if (armadura == 1) //TIRA UN 1
        {
            Debug.Log("Ataque de enfado del enemigo no ha funcionado");

            AutoHerirse(4, 1);
        }
        else
        {
            ActualizarHP();
            NextTurn();
        }
    }
    public void EstadoIntimidar(string name, bool estado)
    {
        switch(name)
        {
            case "enfadado":
                enfadado = estado;
                NextTurn();
                break;
            case "asustado":
                asustado = estado;
                NextTurn();
                break;
        }
    }
    public void AtaqueAsustado(int dado, int times)
    {
        int armadura = Armadura(0, 20);
        asustado = false;
        if (armadura == 2)
        {
            Debug.Log("Ataque de asustado del enemigo ha funcionado");

            int aux = lanzarDado(dado, times);
            int aux_2 = lanzarDado(4, 1);
            int total = Math.Abs(aux - aux_2);
            turnRoundManager.current.Fuerza(turnRoundManager.target, total);
            
            
            ActualizarHP();
            NextTurn();
        }
        else if (armadura == 0) //CRITICO
        {
            Debug.Log("Ataque de asustado del enemigo es critico");

            int aux = lanzarDado(dado, times);
            int aux_2 = lanzarDado(4, 1);
            int aux_3 = lanzarDado(4, 1);
            int total = Math.Abs(aux + aux_2 - aux_3);
            turnRoundManager.current.Fuerza(turnRoundManager.target, total);
            ActualizarHP();
            NextTurn();
        }
        else if (armadura == 1) //TIRA UN 1
        {
            AutoHerirse(4, 1);
        }
        else
        {
        ActualizarHP();
        NextTurn();
        }
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
    // --- DADOS ---
    private int lanzarDado(int caras, int tiradas)
    {
        int a = diceRoller.RollDice(caras, tiradas);
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
        else if (enfadado)
        {
            //Cambiamos turno, y vemos si es el turno del enemigo
            turnRoundManager.ChangeTurn();
            turnRoundManager.AtaqueEnfadado();

        }
        else if ( asustado)
        {
            //Cambiamos turno, y vemos si es el turno del enemigo
            turnRoundManager.ChangeTurn();
            turnRoundManager.AtaqueAsustado();

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
