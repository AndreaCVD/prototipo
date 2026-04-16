using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnRoundManager : MonoBehaviour
{
    //decir a quien le toca
    //Canvas
    [SerializeField] CanvasGroup grup;
    //Textos
    [SerializeField] TMP_Text turnoTexto;

    //variable de combat monster
    public CombatMonster current;
    public CombatMonster target;
    //[SerializeField] CombatMonster opponent;
    //[SerializeField] CombatMonster prota;
    //public GameObject canvas_animado;
    [SerializeField] Animator anim;
    [SerializeField] CombatManager Manager;
    [SerializeField] Menu_Command menu_acciones;

    //NPC
    [SerializeField] NPCAction npcTurn;
    private void Awake()
    {

        //cuando current == prota, es nuestro turno
        current = Manager.playerPersonaje;
        target = Manager.enemyPersonaje;
        Debug.Log("Atacante = " + current);
        Debug.Log("Objetivo = " + target);
        menu_acciones.opacidad(1f);        
        turnoTexto.text = "Es turno de: " + current;
        //if (anim == null)
        //{
        //    //canvas_animado = GameObject.Find("Personajes_Canvas");
        //    anim = canvas_animado.GetComponent<Animator>();
        //}
        anim.SetBool("TurnProta", true);
        anim.SetBool("TurnEnemy", false);
    }
 
    public void ChangeTurn()
    {
        Debug.Log("CAMBIO TURNO");
        //si es el turno del prota, se cambia al enemigo
        if ( current == Manager.playerPersonaje)
        {
            turnoTexto.text = "Es turno de: Enemigo";
            menu_acciones.opacidad(0f);
            //Cambiar current y target
            current = Manager.enemyPersonaje;
            target = Manager.playerPersonaje;
           
            Debug.Log("AtacantE = " + current);
            Debug.Log("Objetivo = " + target);
            //Animacion
            anim.SetBool("TurnProta", false);
            anim.SetBool("TurnEnemy", true);


        }
        //si es turno del enemigo, se cambia al prota
        else if (current == Manager.enemyPersonaje)
        {
            turnoTexto.text = "Es turno de: Prota";
            menu_acciones.opacidad(1f);
            //Cambiar current y target
            current = Manager.playerPersonaje;
            target = Manager.enemyPersonaje;

            Debug.Log("Atacante = " + current);
            Debug.Log("Objetivo = " + target);
            //Animacion
            anim.SetBool("TurnProta", true);
            anim.SetBool("TurnEnemy", false);
            //Accion NPC, NO METER AQUI DA FALLOS
            //npcTurn.DoAction();

        }
    }
    public void EnemyTurn()
    {
        //Si es el turno del Enemigo, hacemos que actue solo
        if (current == Manager.enemyPersonaje)
        {
            Debug.Log("------AHORA ATCATA EL ENEMIGO-----");
            npcTurn.DoAction();
        }
    }
}
