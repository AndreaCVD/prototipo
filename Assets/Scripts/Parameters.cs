using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//para indentificar que stat es
public enum PersonajesStats
{
    Fuerza,         //0
    Inteligencia,   //1
    Carisma,        //2
    Constitucion,   //3
    ClaseArmadura,  //4
    Max_Vida        //5
}

//para poder editarlo en el editor de unity le ponemos serializable
[Serializable]
public class ValueContainer
{
    public int value;
    public PersonajesStats stats;

    public ValueContainer (int value, PersonajesStats stats)
    {
        this.value = value;
        this.stats = stats;
    }
}
[Serializable]
public class ValueBlock
{
    private const int persStatsNum = 5;
    public List<ValueContainer> values;
    public void InitPersonaje()
    {
        values = new List<ValueContainer>();
        for (int i = 0; i < persStatsNum; i++)
        {
            values.Add(new ValueContainer(0, (PersonajesStats)i));
        }
    }

    public int Get(PersonajesStats statToGet)
    {
        int a = (int)statToGet; 
        return values[a].value;
    }
}



[CreateAssetMenu(menuName = "Data/Personaje")]
public class Parameters : ScriptableObject
{
    public string namePers;
    public bool enamorado;
    public ValueBlock stats;
    public Bolsa Inventario;
    //public List<A> X;

    public GameObject modelPrefab;
    public Sprite idle;
    public Sprite herido;

    public List<Sprite> Ataques = new List<Sprite>();
    // ataque [0] -> todos lo tienen && daga prota
    // ataque [1] -> espada prota
    // ataque [2] -> ataque hechizo
    // ataque [3] -> ataque intimidar
    // ataque [4] -> ataque enamorar

    // Lista estados
    public List<Sprite> Estados_combate = new List<Sprite>();
    // [0] atrapado; && PROTA PROTECCION
    // [1] enfadado;
    // [2] asustado;

    public List<Sprite> Estados_enamorado = new List<Sprite>();
    //public Sprite enamorado_1, enamorado_2, enamorado_3;

    //public GameObject variantModel;

    [ContextMenu("Init")]
    public void Init()
    {
        stats = new ValueBlock();
        stats.InitPersonaje();

    }

}

//[CreateAssetMenu(fileName = "Puzzles", menuName = "Puzzle/ListPuzzle")]
//public class Puzzle : ScriptableObject
//{

//    public List<Bools> Nivel_0 = new List<Bools>();
//    public List<Bools> Nivel_1 = new List<Bools>();
//    public List<Bools> Nivel_2 = new List<Bools>();
//    public List<Bools> NivelDesbloqueado = new List<Bools>();

//}