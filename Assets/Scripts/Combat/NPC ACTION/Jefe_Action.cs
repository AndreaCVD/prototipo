using UnityEngine;

public class Jefe_Action : MonoBehaviour
{
    int ataque;
    bool player_inmovilizado, escudo;
    [SerializeField] CommandManager commandManager;

    private void Start()
    {
        player_inmovilizado = false;
        escudo = false;
    }

    public void Ataque_Aleatorio()
    {
        if (player_inmovilizado)
        {
            player_inmovilizado = false;
            ataque = Random.Range(0, 9); //sin inmovilizar
        }
        else if (escudo)
        {
            escudo = false;
            ataque = Random.Range(0, 10);
        }
        else
        {
            //del 0 a 9
            ataque = Random.Range(0, 10);
            if (ataque == 8)
                ataque++;
        }

        switch (ataque)
        {
            case 1:
                Libretazo();
                break;

            case 8:
                Atrapar();
                break;
            case 9:
                Corte();
                break;
            default:
                Debug.Log("Error de lectura");
                break;
        }
    }

    // Fuerza - 1d6+fue
    void Libretazo()
    {
        Debug.Log("Libretazo de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Fuerza(6, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(6, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
    // Fuerza - 1d4+fue
    void Corte()
    {
        Debug.Log("Corte de pagina de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Fuerza(4, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(4, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
    // Inteligencia - durante 1 turno
    void Atrapar()
    {
        Debug.Log("Atrapar de jefe");

    }
    // Carisma - inmovilizado 1 turno y 1d4+carisma
    void Convencer()
    {
        Debug.Log("Convencer de jefe");

    }
    // Inteligencia - 2d6+intel
    void Ataque_X()
    {
        Debug.Log("Ataque X de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Inteligencia(6, 2);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Inteligencia(6, 4);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }

}
