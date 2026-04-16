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
        turnoTexto.text = "Es turno de: ";
        //cuando current == prota, es nuestro turno
        current = Manager.playerPersonaje;
        target = Manager.enemyPersonaje;
        Debug.Log("Atacante = " + current);
        Debug.Log("Objetivo = " + target);
        menu_acciones.opacidad(1f);
        //if (anim == null)
        //{
        //    //canvas_animado = GameObject.Find("Personajes_Canvas");
        //    anim = canvas_animado.GetComponent<Animator>();
        //}
    }
    private void Start()
    {

        anim.SetBool("TurnProta", true);
        anim.SetBool("TurnEnemy", false);
    }
 
    public void ChangeTurn()
    {
        //turno prota
        if ( current == Manager.playerPersonaje)
        {
            turnoTexto.text = "Es turno de: Prota ";

            current = Manager.enemyPersonaje;
            target = Manager.playerPersonaje;
            Debug.Log("Atacante = " + current);
            Debug.Log("Objetivo = " + target);
            anim.SetBool("TurnProta", true);
            anim.SetBool("TurnEnemy", false);
            menu_acciones.opacidad(1f);
        }
        //turno enemigo
        else if (current == Manager.enemyPersonaje)
        {
            turnoTexto.text = "Es turno de: Enemigo";

            current = Manager.playerPersonaje;
            target = Manager.enemyPersonaje;
            Debug.Log("Atacante = " + current);
            Debug.Log("Objetivo = " + target);
            menu_acciones.opacidad(0f);
            anim.SetBool("TurnProta", false);
            anim.SetBool("TurnEnemy", true);

            npcTurn.DoAction();
        }
    }
}
