using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondFloorPuzzle : MonoBehaviour
{
    [SerializeField] Puzzle lista;

    private bool preload;

    [Header("Obj a modificar al acabar puzzle")]
    [SerializeField] Animator puertaMaestra;
    [SerializeField] Animator anim_puertaC3;
    [SerializeField] Animator anim_puertaC6;
    private bool puertaMaestraAbierta;

    [Header("Puzzle_1")]
    [SerializeField] PuzzleZone zona_1;
    [SerializeField] List<GameObject> p1_eliminarObj = new List<GameObject>();
    private bool puerta_c3;
    
    [Header("Puzzle_2a")]
    [SerializeField] PuzzleZone zona_2a;
    [SerializeField] List<GameObject> p2a_eliminarObj = new List<GameObject>();
    private bool nivelAgua;

    [Header("Puzzle_2b")]
    [SerializeField] PuzzleZone zona_2b;
    [SerializeField] List<GameObject> p2b_eliminarObj = new List<GameObject>();
    private bool puerta_c6b;
    [Header("Personajes a instanciar")]
    [SerializeField] List<GameObject> Nim = new List<GameObject>();

    void Start()
    {
        revisarPuzzle();
        InstanciarPers();
        //Treure pq sino sempre es reinicien?
        zona_1.finished = lista.Nivel_2[0].acabado; //Puzzle C3
        zona_2a.finished = lista.Nivel_2[1].acabado; //Puzzle C6_1
        zona_2b.finished = lista.Nivel_2[2].acabado; //Puzzle C6_2
        lista.NivelDesbloqueado[2].acabado = false; //
    }

    void Update()
    {
        //Puzzle C3
        if (!lista.Nivel_2[0].acabado)
        {
            lista.Nivel_2[0].acabado = zona_1.finished;
        }
        else if (!puerta_c3)
        {
            puerta_c3 = true;
            anim_puertaC3.SetBool("doorOpen", true);
        }
        //Puzzle C6_1 --> agua
        if (!lista.Nivel_2[1].acabado)
        {
            lista.Nivel_2[1].acabado = zona_2a.finished;
        }
        else if (!nivelAgua)
        {
            nivelAgua = true;
            Debug.Log("El nivel de agua baja");
            //anim_puertaC3.SetBool("doorOpen", true);
        }
        //Puzzle C6_2 --> llave maestra
        if (!lista.Nivel_2[2].acabado && nivelAgua)
        {
            lista.Nivel_2[2].acabado = zona_2b.finished;
        }
        else if (!puerta_c6b)
        {
            puerta_c6b = true;
            anim_puertaC6.SetBool("doorOpen", true);
        }
        //Puerta Maestra
    }

    //Para C6a --> solo 1 caja tiene que ser true
    //Para C6b --> 2 cajas tienen que ser true
    public void revisarPuzzle()
    {
        //Vemos si hay un puzzle acabado
        for (int i = 0; i < lista.Nivel_1.Count; i++)
        {
            if (lista.Nivel_1[i].acabado)
            {
                switch (lista.Nivel_1[i].name)
                {
                    case "Puzzle_B1":
                        eliminarLista_1("puzz_1");
                        lastPos_1();
                        break;
                    case "Puzzle_B2":
                        eliminarLista_2("puzz_2");
                        break;
                    default:
                        break;
                }
            }
        }

    }

    void eliminarLista_1(string name)
    {
        foreach (var a in p1_eliminarObj)
        {

            Destroy(a);
        }
    }
    void eliminarLista_2(string name)
    {
        foreach (var a in p2a_eliminarObj)
        {
            Destroy(a);
        }
    }
    void lastPos_1()
    {
        //anar per tota la llista y guardar cada posicio
    }
    void InstanciarPers()
    {
        if (!lista.Nivel_2[2].acabado)
        {
            //Si nunca se ha hecho el puzzle B2 etkis no es libre
            Instantiate(Nim[0], Nim[1].transform.position, Nim[1].transform.rotation);
        }
    }
}
