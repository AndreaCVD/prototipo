using UnityEngine;

public class personaje : MonoBehaviour
{
    //como sera siempre un personaje
    //public struct player 
    //{
    // public:
    //    string name;
    //    int salud;
    //    int fuerza;
    //    int inteligencia;
    //    int carisma;
    //};

    //struct uno : player
    //{

    //};

    [Header("Stats")]
    public int constitucion = 10;
    public int fuerza = 10;
    public int carisma = 10;
    public int inteligencia = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int vida()
    {
        return constitucion;
    }
    public int strengh()
    {
        return fuerza;
    }
    public int charm()
    {
        return carisma;
    }
    public int intel()
    {
        return inteligencia;
    }
}
