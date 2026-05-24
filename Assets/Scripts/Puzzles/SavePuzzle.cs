using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[Serializable]
public class Bools
{
    public string name;
    public bool acabado;
}
[Serializable]
public class Puzzles
{
    public string name;
    public List<Bools> Acabado = new List<Bools>();
    //Si tenen mes de 1 peça
    public List<GameObject> SitioFijo = new List<GameObject>();
}
//NO BORRAR LO DE ARRIBA

public class SavePuzzle : MonoBehaviour
{
    public Puzzle lista;
    private Preload preload;

    void Start()
    {
        //Mirar cada uno de los puzzles
        for (int i = 0; i < lista.First_Floor.Count; i++)
        {
            foreach (var puzzle in lista.First_Floor[i].Acabado)
            {
                puzzle.acabado = false;
            }
        }
        for (int i = 0; i < lista.Second_Floor.Count; i++)
        {
            foreach (var puzzle in lista.First_Floor[i].Acabado)
            {
                puzzle.acabado = false;
            }
        }
        pieceDone("b3_1");
    }
    //Poner en el sitio correcto
    public void revisarLista()
    {
        //Scene escenaActual = SceneManager.GetActiveScene();
        //switch (escenaActual.name)
        //{
        //    case "first_floor":
        //        firstFloor();
        //        break;
        //    case "second_floor":
        //        //floor = 1;
        //        break;
        //    default:
        //        break;
        //}
    }

    public void pieceDone(string name)
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        switch(escenaActual.name)
        {
            case "first_floor":
                firstFloor(name);
                break;
            case "second_floor":
                secondFloor(name);
                break;
            default:
                break;
        }
    }
    void firstFloor(string name)
    {
        for (int i = 0; i < lista.First_Floor.Count; i++)
        {
            foreach (var puzzle in lista.First_Floor[i].Acabado)
            {
                if (puzzle.name == name) //Si conincide nombre con pieza
                {
                    puzzle.acabado = true;
                    break;
                }
            }
        }
    }
    void secondFloor(string name)
    {
        for (int i = 0; i < lista.Second_Floor.Count; i++)
        {
            foreach (var puzzle in lista.Second_Floor[i].Acabado)
            {
                Debug.Log("Llega?");
                if (puzzle.name == name) //Si conincide nombre con pieza
                {
                    puzzle.acabado = true;
                    break;
                }
            }
        }
    }

    //La recompensa si es fa el puzzle

    //Puzzle 1 --> que la llave y el cofre desaparezcan si se abren
    //Puzzle 2 --> las dos cajas en el interruptor
}
