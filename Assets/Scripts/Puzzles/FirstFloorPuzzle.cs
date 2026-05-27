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
    [Header("Puzzle_1")]
    [SerializeField] PuzzleZone zona_1;
    [SerializeField] List<GameObject> p1_eliminarObj = new List<GameObject>();

    [Header("Puzzle_2")]
    [SerializeField] PuzzleZone zona_2;
    [SerializeField] List<GameObject> p2_eliminarObj = new List<GameObject>();

    void Start()
    {
        revisarPuzzle();

        zona_1.finished = lista.Second_Floor[0].acabado;
        zona_1.finished = lista.Second_Floor[1].acabado;

    }

    void Update()
    {
        if (!lista.Second_Floor[0].acabado)
        {
            lista.Second_Floor[0].acabado = zona_1.finished;
        }
        if (!lista.Second_Floor[1].acabado)
        {
            lista.Second_Floor[1].acabado = zona_2.finished;
        }

    }


    public void revisarPuzzle()
    {
        //Vemos si hay un puzzle acabado
        for (int i = 0; i < lista.Second_Floor.Count; i++)
        {
            Debug.Log(lista.Second_Floor[i].acabado);
            if (lista.Second_Floor[i].acabado)
            {
                switch (lista.Second_Floor[i].name)
                {
                    case "Puzzle_1":
                        eliminarLista_1("puzz_1");
                        lastPos_1();
                        break;
                    case "Puzzle_2":
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
