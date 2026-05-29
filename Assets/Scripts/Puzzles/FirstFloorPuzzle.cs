using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstFloorPuzzle : MonoBehaviour
{
    [SerializeField] Puzzle lista;

    private bool preload;

    [Header("Obj a modificar al acabar puzzle")]
    [SerializeField] Animator puertaMaestra;
    [SerializeField] Animator jaulaEtkis;
    private bool puertaAbierta;
    private bool jaulaAbierta;
    [Header("Puzzle_1")]
    [SerializeField] PuzzleZone zona_1;
    [SerializeField] List<GameObject> p1_eliminarObj = new List<GameObject>();

    [Header("Puzzle_2")]
    [SerializeField] PuzzleZone zona_2;
    [SerializeField] List<GameObject> p2_eliminarObj = new List<GameObject>();
    
    void Start()
    {
        revisarPuzzle();
        //Treure pq sino sempre es reinicien?
        zona_1.finished = lista.Nivel_1[0].acabado;
        zona_1.finished = lista.Nivel_1[1].acabado;
        lista.NivelDesbloqueado[1].acabado = false;
    }

    void Update()
    {
        if (!lista.Nivel_1[0].acabado)
        {
            lista.Nivel_1[0].acabado = zona_1.finished;
        }
        if (!lista.Nivel_1[1].acabado)
        {
            lista.Nivel_1[1].acabado = zona_2.finished;
        }
        if (lista.NivelDesbloqueado[1].acabado && !puertaAbierta)
        {
            puertaAbierta = true;
            puertaMaestra.SetBool("doorOpen", true);
        }
        if (lista.Nivel_1[1].acabado && !jaulaAbierta) //Completar puzzle B2
        {
            jaulaAbierta = true;
            jaulaEtkis.SetBool("barrotesOpen", true);
        }
    }


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
        foreach (var a in p2_eliminarObj)
        {
            Destroy(a);
        }
    }
    void lastPos_1()
    {
        //anar per tota la llista y guardar cada posicio
    }
}
