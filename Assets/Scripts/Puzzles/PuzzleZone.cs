using System.Collections;
using System;

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleZone : MonoBehaviour
{
    [SerializeField] GameObject player;
    public bool playerInside;    
    public bool restart;
    public bool finished;
    //Canvas
    [SerializeField] CanvasGroup grup;

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

    void Start()
    {

        player = GameObject.Find("personaje");

        playerInside = false;

        if (script_dialog == null)
        {
            script_dialog = GameObject.Find("--DialogManager--");
            dialog = script_dialog.GetComponent<Dialog>();
        }

        opacidad(0f);
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
            restart = Input.GetKeyDown(KeyCode.R);
            //Mirar si barra espaciadora esta activada
            if (restart)
            {
                Debug.Log("Reinicio");
                RestartPuzzle();
            }
        }
        //---Puzzle---
        if (!finished)
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
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
             //Enseñar canvas
           
            if (!playerInside)
            {
                playerInside = true;
                opacidad(1f);
                if ( dialogOnEnter && !finished)
                {

                    dialog.EmpezarDialogo(dialogo_obj, this.gameObject);
                }
            }
            else
            {
                playerInside = false;
                opacidad(0f);

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
    public void opacidad(float nueva_opacidad)
    {
        grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
}
