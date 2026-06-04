using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Puzzles", menuName = "Puzzle/ListPuzzle")]
public class Puzzle : ScriptableObject
{
    public List<Bools> Nivel_0 = new List<Bools>();
    public List<Bools> Nivel_1 = new List<Bools>();
    public List<Bools> Nivel_2 = new List<Bools>();
    public List<Bools> NivelDesbloqueado = new List<Bools>();
}