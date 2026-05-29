using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFloorPuzzle : MonoBehaviour
{
    [SerializeField] Puzzle lista;

    private bool preload;
    private int tutorial = 0;

    [Header("Obj a modificar al acabar puzzle")]
    [SerializeField] Animator puertaMazmorra;
    [SerializeField] Animator candado_1;

    private bool puertaAbierta;
    [Header("Puzzle_1")]
    [SerializeField] PuzzleZone zona_1;
    [SerializeField] List<GameObject> p1_eliminarObj = new List<GameObject>();

    [Header("Personajes a instanciar")]
    [SerializeField] List<GameObject> Viejo = new List<GameObject>();

    [SerializeField] GameObject Etkis; //Mirar Puzzle B2
    [SerializeField] GameObject Nim; //Mirar Puzzle C6_2
    [SerializeField] GameObject Lerendur; //Ganar pelea final

    void Start()
    {
        InstanciarPers();
        revisarPuzzle();
        //Treure pq sino sempre es reinicien?
        lista.Nivel_0[0].acabado = false;
        zona_1.finished = lista.Nivel_0[0].acabado;
        lista.NivelDesbloqueado[0].acabado = false;
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
            candado_1.SetBool("candadoOpen", true);
            puertaMazmorra.SetBool("doorOpen", true);
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
        if (lista.NivelDesbloqueado[0].acabado)
        {
            //Se instancia viejo de playa
            Instantiate(Viejo[2], Viejo[3].transform.position, Viejo[3].transform.rotation);
        }
        else
        {
            Instantiate(Viejo[0], Viejo[1].transform.position, Viejo[1].transform.rotation);

        }
    }
}
