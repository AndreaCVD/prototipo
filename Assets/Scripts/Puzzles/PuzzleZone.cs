using System.Collections;
using System;

using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PuzzleZone : MonoBehaviour
{
    public List<GameObject> Children = new List<GameObject>();

    [SerializeField] List<GameObject> PiezasPuzzles = new List<GameObject>();
    [SerializeField] List<GameObject> SitioReinicio = new List<GameObject>();

    public bool restart;  // To track if the player presses the jump key

    void Start()
    {

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
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player ha entrado en la zona");
            Debug.Log("1. Dar opcion de reseteo");



        }
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            
            // BOTON REINICIO 
            restart = Input.GetKeyDown(KeyCode.R);
            //Mirar si barra espaciadora esta activada
            if (restart)
            {
                Debug.Log("Reinicio");
                RestartPuzzle();
            }

            //Opacidad(1f)
        }
    }
    void OnTriggerLeave()
    {
        //Hablar con preload para guardar posiciones si el puzzle se ha cumplido
        //Opacidad(0f)
    }

    void RestartPuzzle()
    {
        for (int i = 0; i < PiezasPuzzles.Count; i++)
        {
            GameObject pieza = PiezasPuzzles[i];
            GameObject trans = SitioReinicio[i];

            //pieza.transform = 
        }
    }
}
