using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

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

    public Parameters prota;
    public Puzzle lista;


    void Awake()
    {
        for (int i = 0; i < lista.Nivel_1.Count; i++)
        {
            //Debug.Log("Puzzles todos false");
            lista.Nivel_1[i].acabado = false;

        }
    }

}
    //La recompensa si es fa el puzzle

    //Puzzle 1 --> que la llave y el cofre desaparezcan si se abren
    //Puzzle 2 --> las dos cajas en el interruptor