using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TurnRoundManager : MonoBehaviour
{
    //decir a quien le toca
    //Canvas
    [SerializeField] CanvasGroup grup;
    //Textos
    [SerializeField] TMP_Text turnoTexto;

    //UI
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    VisualElement combat_options;
    Label turno;

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

    private void OnEnable()
    {
        // 1. Usar la variable serializada 'uIDocument' que ya tienes declarada arriba
        // en lugar de intentar un GetComponent que puede fallar.
        if (uIDocument == null)
        {
            uIDocument = GetComponent<UIDocument>();
        }

        // 2. Verificación de seguridad
        if (uIDocument != null && uIDocument.rootVisualElement != null)
        {
            root = uIDocument.rootVisualElement;
            combat_options = root.Q<VisualElement>("combat_options");
            turno = root.Q<Label>("turno");

            if (combat_options == null)
            {
                Debug.LogWarning("No se encontró el elemento 'combat_options' en el UXML.");
            }
        }
        else
        {
            Debug.LogError("TurnRoundManager: No hay un UIDocument asignado o el root está vacío.");
        }
    }

    private void Start()
    {

        //cuando current == prota, es nuestro turno
        current = Manager.playerPersonaje;
        target = Manager.enemyPersonaje;

        //Debug.Log("Atacante = " + current);
        //Debug.Log("Objetivo = " + target);
        
        anim.SetBool("TurnProta", true);
        anim.SetBool("TurnEnemy", false);

        //menu_acciones.opacidad(1f);

        turno.text = "Es turno de: " + current;

        //turnoTexto.text = "Es turno de: " + current;
        //if (anim == null)
        //{
        //    //canvas_animado = GameObject.Find("Personajes_Canvas");
        //    anim = canvas_animado.GetComponent<Animator>();
        //}

    }
 
    public void ChangeTurn()
    {


        Debug.Log("CAMBIO TURNO");
        //si es el turno del prota, se cambia al enemigo
        if ( current == Manager.playerPersonaje)
        {

            turno.text = "Es turno de: Enemigo";

            //turnoTexto.text = "Es turno de: Enemigo";
            //menu_acciones.opacidad(0f);

            //Cambiar current y target
            current = Manager.enemyPersonaje;
            target = Manager.playerPersonaje;
           
            //Debug.Log("AtacantE = " + current);
            //Debug.Log("Objetivo = " + target);

            //Animacion
            anim.SetBool("TurnProta", false);
            anim.SetBool("TurnEnemy", true);

            //Ocultar el menu de combate
            combat_options.style.display = DisplayStyle.None;

        }
        //si es turno del enemigo, se cambia al prota
        else if (current == Manager.enemyPersonaje)
        {
            turno.text = "Es turno de: Prota";

            //turnoTexto.text = "Es turno de: Prota";
            //menu_acciones.opacidad(1f);

            //Cambiar current y target
            current = Manager.playerPersonaje;
            target = Manager.enemyPersonaje;

            //Debug.Log("Atacante = " + current);
            //Debug.Log("Objetivo = " + target);

            //Animacion
            anim.SetBool("TurnProta", true);
            anim.SetBool("TurnEnemy", false);
            //Accion NPC, NO METER AQUI DA FALLOS
            //npcTurn.DoAction();

            //Mostrar el menu de las opciones de combate
            combat_options.style.display = DisplayStyle.Flex;

        }
    }
    public void EnemyTurn()
    {
        //Si es el turno del Enemigo, hacemos que actue solo
        if (current == Manager.enemyPersonaje)
        {
            //Debug.Log("------AHORA ATCATA EL ENEMIGO-----");
            npcTurn.DoAction();
        }
    }
}
