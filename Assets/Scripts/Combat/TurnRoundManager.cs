using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRoundManager : MonoBehaviour
{
    //decir a quien le toca

    //variable de combat monster
    public CombatMonster current;
    public CombatMonster target;

    //[SerializeField] CombatMonster opponent;
    //[SerializeField] CombatMonster prota;
    public Animator animation;
    [SerializeField] CombatManager Manager;
    [SerializeField] Menu_Command menu_acciones;
    private void Awake()
    {
        //cuando current == prota, es nuestro turno
        current = Manager.playerPersonaje;
        target = Manager.enemyPersonaje;
        Debug.Log("Atacante = " + current);
        Debug.Log("Objetivo = " + target);
        menu_acciones.opacidad(1f);
        animation.SetBool("TurnProta", true);
        animation.SetBool("TurnEnemy", false);

    }
    public void ChangeTurn()
    {
        //turno prota
        if ( current == Manager.playerPersonaje)
        {
            current = Manager.enemyPersonaje;
            target = Manager.playerPersonaje;
            Debug.Log("Atacante = " + current);
            Debug.Log("Objetivo = " + target);
            animation.SetBool("TurnProta", true);
            animation.SetBool("TurnEnemy", false);
            menu_acciones.opacidad(1f);
        }
        //turno enemigo
        else if (current == Manager.enemyPersonaje)
        {
            current = Manager.playerPersonaje;
            target = Manager.enemyPersonaje;
            Debug.Log("Atacante = " + current);
            Debug.Log("Objetivo = " + target);
            animation.SetBool("TurnProta", false);
            animation.SetBool("TurnEnemy", true);
            menu_acciones.opacidad(0f);
        }
    }
}
