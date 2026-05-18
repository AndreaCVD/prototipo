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
    //Canvas
    [SerializeField] CanvasGroup grup;

    [Header("Listas")]
    public List<GameObject> Children = new List<GameObject>();

    [SerializeField] List<GameObject> PiezasPuzzles = new List<GameObject>();
    [SerializeField] List<GameObject> SitioReinicio = new List<GameObject>();
    [SerializeField] Transform pos_player;


    void Start()
    {
        player = GameObject.Find("personaje");

        playerInside = false;

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
            foreach (GameObject a in Children)
            {
                GameObject b = a.transform.GetChild(i).gameObject;

                if (b.name.Contains("_pos"))
                {
                    SitioReinicio.Add(b);//Posicion Original
                }
                else if (b.name.Contains("player"))
                {
                    pos_player = b.transform;
                }
                else if (b.name.Contains("_"))
                {
                    PiezasPuzzles.Add(b);//Piezas
                }
                Debug.Log(b);
            }
        }

    }
    private void FixedUpdate()
    {
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
            GameObject trans = SitioReinicio[i];

            pieza.transform.position = new Vector3(trans.transform.position.x, trans.transform.position.y, trans.transform.position.z );
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
