using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFloorPuzzle : MonoBehaviour
{
    [SerializeField] Puzzle lista;

    private bool preload;

    [Header("Obj a modificar al acabar puzzle")]
    [SerializeField] Animator puertaMazmorra;
    [SerializeField] Animator candado_1;

    private bool puertaAbierta;
    [Header("Puzzle_1")]
    [SerializeField] PuzzleZone zona_1;
    [SerializeField] List<GameObject> p1_eliminarObj = new List<GameObject>();

    [Header("Personajes a instanciar")]
    [SerializeField] List<GameObject> Viejo = new List<GameObject>();
    [SerializeField] List<GameObject> Etkis = new List<GameObject>(); //Mirar Puzzle B2
    [SerializeField] List<GameObject> Nim = new List<GameObject>();//Mirar Puzzle C6_2
    [SerializeField] List<GameObject> Lerendur = new List<GameObject>();//Ganar pelea final

    void Start()
    {
        InstanciarPers();
        revisarPuzzle();

        zona_1.finished = lista.Nivel_0[0].acabado;
    }

    void Update()
    {
        if (!lista.Nivel_0[0].acabado)
        {
            lista.Nivel_0[0].acabado = zona_1.finished;
        }

        if (lista.Nivel_0[0].acabado && !puertaAbierta)
        {
            puertaAbierta = true;
            lista.NivelDesbloqueado[0].acabado = true;
            //Animacion Candado y eliminarlo
            if (puertaMazmorra != null)
            {
                candado_1.SetBool("candadoOpen", true);
                puertaMazmorra.SetBool("doorOpen", true);
            }

        }

    }


    public void revisarPuzzle()
    {
        //Vemos si hay un puzzle acabado
        for (int i = 0; i < lista.Nivel_0.Count; i++)
        {
            if (lista.Nivel_0[i].acabado)
            {
                switch (lista.Nivel_0[i].name)
                {
                    case "Puzzle_A1":
                        eliminarLista_1();
                        break;

                    default:
                        break;
                }
            }
        }

    }

    void eliminarLista_1()
    {
        foreach (var a in p1_eliminarObj)
        {

            Destroy(a);
        }
    }
    
    void InstanciarPers()
    {
        //Viejo
        if (lista.NivelDesbloqueado[0].acabado)
        {
            //Se instancia viejo de playa
            Instantiate(Viejo[2], Viejo[3].transform.position, Viejo[3].transform.rotation);
        }
        else
        {
            Instantiate(Viejo[0], Viejo[1].transform.position, Viejo[1].transform.rotation);

        }
        //Etkis --> Mirar Puzzle B2
        if (lista.Nivel_1[1].acabado)
        {
            //Se instancia Etkis
            Instantiate(Etkis[0], Etkis[1].transform.position, Etkis[1].transform.rotation);
        }
        //Nim --> Mirar Puzzle C6_2
        if (lista.Nivel_2[2].acabado) //Nivel Desbloqueado [3]
        {
            //Se instancia Nim
            Instantiate(Nim[0], Nim[1].transform.position, Nim[1].transform.rotation);
        }
        //Lerendur --> Ganar pelea final
        if (lista.NivelDesbloqueado[3].acabado)
        {
            Instantiate(Lerendur[2], Lerendur[3].transform.position, Lerendur[3].transform.rotation);
        }
    }
}
