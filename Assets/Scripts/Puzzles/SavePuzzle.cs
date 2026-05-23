using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Puzzles
{
    public string name;
    [SerializeField] List<bool> SitioFijo = new List<bool>();
    //Si tenen mes de 1 peça
    [SerializeField] List<GameObject> SitioFijo = new List<GameObject>();
}

public class SavePuzzle : MonoBehaviour
{
    [SerializeField] List<Puzzles> ListaPuzzles = new List<Puzzles>();
    Preload preload;

    //Poner en el sitio correcto
    public void isPuzzleDone()
    {

        foreach (var puzzle in ListaPuzzles)
        {
        }
    }
    
    //La recompensa si es fa el puzzle
}
