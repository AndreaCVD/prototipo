using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//para indentificar que stat es
public enum PlayerStats
{
    Fuerza,         //0
    Inteligencia,   //1
    Carisma,        //2
    Constitucion    //3
}

// para poder editarlo en el editor de unity le ponemos serializable
[Serializable]
public class Valores
{
    public int value;
    public PersonajesStats stats;

    public Valores(int value, PersonajesStats stats)
    {
        this.value = value;
        this.stats = stats;
    }
}

[Serializable]
public class ValoresBlock
{
    private const int persStatsNum = 4;
    public List<Valores> values;
    public void InitPersonaje()
    {
        values = new List<Valores>();
        for (int i = 0; i < persStatsNum; i++)
        {
            values.Add(new Valores(0, (PersonajesStats)i));
        }
    }

    public int Get(PersonajesStats statToGet)
    {
        return values[(int)statToGet].value;
    }
}
[CreateAssetMenu(menuName="Data/Player Data")]
public class PlayerData : ScriptableObject
{
    public string characterName = "Nameless";
    public ValoresBlock stats;

    public GameObject modelPrefab;

    [ContextMenu("Init")]
    public void Init()
    {
        stats = new ValoresBlock();
        stats.InitPersonaje();
    }

}
