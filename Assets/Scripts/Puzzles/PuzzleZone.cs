using System.Collections;
using System;

using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleZone : MonoBehaviour
{
    public bool playerInside;    
    public bool restart;
    //Canvas
    [SerializeField] CanvasGroup grup;

    [Header("Listas")]
    public List<GameObject> Children = new List<GameObject>();

    [SerializeField] List<GameObject> PiezasPuzzles = new List<GameObject>();
    [SerializeField] List<GameObject> SitioReinicio = new List<GameObject>();



    void Start()
    {
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
                else       
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
            Debug.Log("Player ha entrado en la zona");
            Debug.Log("1. Dar opcion de reseteo");
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
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            



        }
    }
    void OnTriggerExit()
    {
        //Hablar con preload para guardar posiciones si el puzzle se ha cumplido
        //opacidad(0f);

    }

    void RestartPuzzle()
    {
        for (int i = 0; i < PiezasPuzzles.Count; i++)
        {
            GameObject pieza = PiezasPuzzles[i];
            GameObject trans = SitioReinicio[i];

            pieza.transform.position = new Vector3(trans.transform.position.x, trans.transform.position.y, trans.transform.position.z );
            Debug.Log(pieza + "- se ha movido a - " + trans);
        }
    }
    //Aqui se reciben las nuevas opacidades
    public void opacidad(float nueva_opacidad)
    {
        grup.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
}
