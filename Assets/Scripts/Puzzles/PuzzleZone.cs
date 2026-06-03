using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
//using UnityEngine.UI;
using UnityEngine.UIElements;

public class PuzzleZone : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool playerInside;    
    public bool restart;
    public bool finished;

    //Canvas
    //[SerializeField] CanvasGroup grup;
    private Button btnReset;

    private List<GameObject> Children = new List<GameObject>();
    [Header("Listas")]
    public List<GameObject> PiezasPuzzles = new List<GameObject>();
    [SerializeField] List<Transform> SitioReinicio = new List<Transform>();
    [SerializeField] Transform pos_player;

    //Para encontrar los scripts de DialogManager
    private Dialog dialog;
    private GameObject script_dialog;
    [Header("EL DIALOGO DEL OBJ")]
    [SerializeField] cherrydev.DialogNodeGraph dialogo_obj;
    //Hi ha habitacions on es necesita que el dialeg es dispari nomes si el puzzle no s'ha fet
    [SerializeField] bool dialogOnEnter;

    [Header("Puzzle de 1 interruptor")]
    [SerializeField] bool cajasExtras;
    private bool zona_2_finished;
    [SerializeField] PuzzleZone zona_b;
    //Puzzle C6a --> solo 1
    //Puzzle C6b --> solo 1 + C6a

    void Start()
    {

        var root = GameObject.Find("UI_HUB").GetComponent<UIDocument>().rootVisualElement;
        btnReset = root.Q<Button>("btn-reset");
        btnReset.style.display = DisplayStyle.None;
        btnReset.clicked += RestartPuzzle; // acción al pulsar

        player = GameObject.Find("personaje");

        playerInside = false;

        if (script_dialog == null)
        {
            script_dialog = GameObject.Find("--DialogManager--");
            dialog = script_dialog.GetComponent<Dialog>();
        }

        //opacidad(0f);

        //Localizar los primeros hijos
        foreach (Transform child in transform)
        {
            GameObject childObj = child.gameObject;
            Children.Add(childObj);
        }
        //Ahora a los nietos
        int lenght = Children.Count;
        for (int i = 0;  i < lenght; i++)
        {
            foreach (Transform a in Children[i].transform)
            {
                GameObject b = a.gameObject;

                if (b.name.Contains("_pos"))
                {
                    SitioReinicio.Add(b.transform);//Posicion Original
                }
                else if (b.name.Contains("player"))
                {
                    pos_player = b.transform;
                }
                else if (b.name.Contains("_"))
                {
                    PiezasPuzzles.Add(b);//Piezas

                }
                //Debug.Log(b);
            }
        }

    }
    private void FixedUpdate()
    {
        //reinicio puzzle
        if (playerInside)
        {
            // BOTON REINICIO 
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Reinicio");
                RestartPuzzle();
            }
        }
        //---Puzzle---
        if (!finished && Children.Count != 0 && !cajasExtras)
        {
            for (int i = 0; i < PiezasPuzzles.Count; i++)
            {
                if (!PiezasPuzzles[i].GetComponent<EmpujarObjetos>().returnState())
                {
                    return;
                }
            }
            finished = true;
            Debug.Log("Se ha terminado el puzzle de la Zona");

        }
        //---Puzzle Secundario---
        if (!finished && Children.Count != 0 && cajasExtras)
        {
            int falsasCajas = 0;
            for (int i = 0; i < PiezasPuzzles.Count; i++)
            {
                //Al menos que una pieza sea TRUE
                if (!PiezasPuzzles[i].GetComponent<EmpujarObjetos>().returnState())
                {
                    falsasCajas++;
                }                    
            }
            if (falsasCajas != PiezasPuzzles.Count)
            {
                finished = true;
            }
        }
        else if (!zona_2_finished && cajasExtras && zona_b != null)
        {
            int falsasCajas = 0;
            for (int i = 0; i < PiezasPuzzles.Count; i++)
            {
                //Al menos que una pieza sea TRUE
                if (!PiezasPuzzles[i].GetComponent<EmpujarObjetos>().returnState())
                {
                    falsasCajas++;
                }
            }
            if (falsasCajas != PiezasPuzzles.Count - 1)
            {
                zona_b.finished = true;
                zona_2_finished = true;
                Debug.Log("Se ha hecho la Zona B");
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            if (!playerInside)
            {
                playerInside = true;
                btnReset.style.display = DisplayStyle.Flex; // mostrar botón
                if (dialogOnEnter && !finished)
                {
                    dialog.EmpezarDialogo(dialogo_obj, this.gameObject);
                }
            }
            else
            {
                playerInside = false;
                btnReset.style.display = DisplayStyle.None; // ocultar botón
            }
        }

    }

    public void PositionPlayer()
    {
        player.transform.position = new Vector3(pos_player.position.x, pos_player.position.y, pos_player.position.z);
    }

    void RestartPuzzle()
    {
        //Piezas
        for (int i = 0; i < PiezasPuzzles.Count; i++)
        {
            GameObject pieza = PiezasPuzzles[i];
            Transform trans = SitioReinicio[i];
            if (PiezasPuzzles[i].GetComponent<EmpujarObjetos>().returnState())
            {
                PiezasPuzzles[i].GetComponent<EmpujarObjetos>().restartState();
            }
            pieza.transform.position = new Vector3(trans.position.x, trans.position.y, trans.position.z );
            Debug.Log(pieza + "- se ha movido a - " + trans);
        }
        //Jugador
        PositionPlayer();
    }
    //Aqui se reciben las nuevas opacidades
    //public void opacidad(float nueva_opacidad)
    //{
    //    grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    //}

}
