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
    public bool player_inmovilizado = false;
    public bool player_escudo = false;
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
        if (enemigo_inLove == true)
            Change_img("enamorado_3");
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
    public void Fuerza(int dado_1, int times_1, int dado_2, int times_2) //dos dados diferentes
    {
        int aux = lanzarDado(dado_1, times_1);
        int aux_2 = lanzarDado(dado_2, times_2);

        turnRoundManager.current.Fuerza(turnRoundManager.target, aux+aux_2);

        ActualizarHP();

        NextTurn();
    }
    public void Carisma(int dado, int times)
    {
        int aux = lanzarDado(dado, times);

        //Acci�n
        turnRoundManager.current.Carisma(turnRoundManager.target, aux);
        ActualizarHP();

        NextTurn();
    }
    public void Inteligencia(int dado, int times)
    {
        int aux = lanzarDado(dado, times);

        turnRoundManager.current.Inteligencia(turnRoundManager.target, aux);
        
        ActualizarHP();
        NextTurn();
    }
    public void Modificar_CA(int valor)
    {
        Debug.Log("Se ha modificado la CA");
        turnRoundManager.current.modificar_CA(valor);
        ActualizarHP();

        //NextTurn();
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
            turnRoundManager.current.Cambiar_Idle();

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

            turnRoundManager.current.Cambiar_Idle();
            turnRoundManager.current.Fuerza(turnRoundManager.target, aux + aux_2 +aux_3);

            ActualizarHP();
            NextTurn();
        }
        else if (armadura == 1) //TIRA UN 1
        {
            Debug.Log("Ataque de enfado del enemigo no ha funcionado");
            turnRoundManager.current.Cambiar_Idle();
            AutoHerirse(4, 1);
        }
        else
        {
            turnRoundManager.current.Cambiar_Idle();
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
                //NextTurn();
                break;
            case "asustado":
                asustado = estado;
               // NextTurn();
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

            turnRoundManager.current.Cambiar_Idle();
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
            
            turnRoundManager.current.Cambiar_Idle();
            ActualizarHP();
            NextTurn();
        }
        else if (armadura == 1) //TIRA UN 1
        {
            turnRoundManager.current.Cambiar_Idle();
            AutoHerirse(4, 1);
        }
        else
        {
            turnRoundManager.current.Cambiar_Idle();
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
        Change_img("enamorado_3");
        turnRoundManager.target.Enamorado();

    }
    public void EnemigoInmovilizado(bool inmov, int turnos)
    {
        enemigo_inmovilizado = inmov;
        turnos_inmovil = turnos;
    }
    public void PlayerInmovilizado(bool inmov, int turnos)
    {
        player_inmovilizado = inmov;
        turnos_inmovil = turnos;
        if (player_inmovilizado)
            Change_img("inmovil_prota");
    }
    public bool Return_Inmovil(string name)
    {
        if (name == "player")
        {
            return player_inmovilizado;
        }
        else
        {
            return enemigo_inmovilizado;
        }
    }

    // --- DADOS ---
    private int lanzarDado(int caras, int tiradas)
    {
        int a = diceRoller.RollDice(caras, tiradas);
        return a; 
    }
    // --- IMG COMBATE -- 
    public void Change_img(string ataque)
    {

            switch (ataque)
            {
                case "idle_prota":
                    turnRoundManager.current.Cambiar_Idle();
                    break;
                case "idle_enemy":
                    turnRoundManager.target.Cambiar_Idle();
                    break;
                case "daga": //prota daga[0] - enemy herido
                    turnRoundManager.target.Cambiar_imgHerido();
                    turnRoundManager.current.Cambiar_imgAtaque(0);
                    break;
                case "espada": //prota espada[1] - enemy herido
                    turnRoundManager.target.Cambiar_imgHerido();
                    turnRoundManager.current.Cambiar_imgAtaque(1);
                    break;
                case "escudo":
                    player_escudo = true;
                    turnRoundManager.current.Cambiar_imgEstado(0);
                    break;
                case "enamorado_1": 
                    turnRoundManager.current.Cambiar_imgAtaque(4);
                    turnRoundManager.target.Cambiar_imgEnamorado(0);
                    break;
                case "enamorado_2":
                    turnRoundManager.current.Cambiar_imgAtaque(4);
                    turnRoundManager.target.Cambiar_imgEnamorado(1);
                    break;
                case "enamorado_3": 
                    turnRoundManager.target.Cambiar_imgEnamorado(2);
                    break;
                case "inmovil_prota":
                    Debug.Log(" PONER IMAGEN PROTA INMOVILIZADO");    
                    break;
                case "inmovil_enemy": 
                    turnRoundManager.target.Cambiar_imgEstado(0);
                    break;
                case "enfadado": 
                    turnRoundManager.current.Cambiar_imgEstado(1);
                    break;
                case "asustado": 
                    turnRoundManager.target.Cambiar_imgEstado(2);
                    break;
                default:
                    Debug.Log("por default");
                    turnRoundManager.current.Cambiar_Idle();
                    turnRoundManager.target.Cambiar_Idle();
                    break;
            }
        

    }
    public void NextTurn()
    {
        //estados enemigo
        if (enemigo_inmovilizado)
        {
            //No cambiamos el turno
            //Activar animacion 
            turnos_inmovil--;
            Debug.Log("Enemigo Inmovilizado. Le quedan = " + turnos_inmovil);
            if (turnos_inmovil == 0)
            {
                Change_img("idle_enemy");
                Debug.Log("Ya no esta inmovilizado");
                enemigo_inmovilizado = false;
                turnos_inmovil = 1;
                turnRoundManager.ChangeTurn();
                turnRoundManager.EnemyTurn();
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
        //estados jugador
        else if (player_inmovilizado)
        {
            //No cambiamos el turno
            //Activar animacion 
            turnos_inmovil--;
            Debug.Log("Enemigo Inmovilizado. Le quedan = " + turnos_inmovil);
            if (turnos_inmovil == 0)
            {
                Change_img("idle_player");

                Debug.Log("Ya no esta inmovilizado");
                player_inmovilizado = false;
                turnos_inmovil = 1;
            }
            turnRoundManager.EnemyTurn();
        }
        //else if (player_escudo)
        //{
        //    Change_img("idle_player");
        //    Modificar_CA(-2);
        //    turnRoundManager.ChangeTurn();
        //    turnRoundManager.EnemyTurn();
        //}
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
