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
    ClaseArmadura   //4
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
    private const int persStatsNum = 4;
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
    public ValueBlock stats;

    public GameObject modelPrefab;
    public Sprite art;
    //public GameObject variantModel;

    [ContextMenu("Init")]
    public void Init()
    {
        stats = new ValueBlock();
        stats.InitPersonaje();
    }
}
