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
    private FirstFloorPuzzle firstFloor;

    void Awake()
    {
        for (int i = 0; i < lista.Second_Floor.Count; i++)
        {
            //aquest escript no s'ha de destruir, o fer-ho en una altre part
            lista.Second_Floor[i].acabado = false;

        }
    }

}



    //La recompensa si es fa el puzzle

    //Puzzle 1 --> que la llave y el cofre desaparezcan si se abren
    //Puzzle 2 --> las dos cajas en el interruptor

