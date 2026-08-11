using UnityEngine;

public class Slime_Action : MonoBehaviour
{
    int ataque;
    bool player_inmovilizado;
    [SerializeField] CommandManager commandManager;

    private void Start()
    {
        player_inmovilizado = false;
    }
    
    public void Ataque_Aleatorio()
    {
        if (player_inmovilizado)
        {
            player_inmovilizado = false;
            ataque = Random.Range(0, 9); //sin atrapar
        }
        else
        {
            //del 0 a 8
            ataque = Random.Range(0, 10);
        }

        switch (ataque)
        {
            case 1:
                Escupir();
                break;
            case 9:
                Atrapar();
                break;
            default:
                Debug.Log("Error de lectura");
                break;
        }
    }
    void Escupir() // 1d6 + fue +1d4
    {
        Debug.Log("Escupir de slime");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            //Fuerza(int dado_1, int times_1, int dado_2, int times_2)
            commandManager.Fuerza(6, 1, 4, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(6, 2, 4, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
    // Inteligencia - Atrapar, inmovilizar 1 turno
    void Atrapar()
    {
        Debug.Log("Atrapar de slime + Acido");
        player_inmovilizado = true;
        commandManager.PlayerInmovilizado(true);
        
        int ca_player = commandManager.Armadura(0, 20);
        if (ca_player == 2) //supera armadura
        {
            commandManager.Inteligencia(4, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Inteligencia(4, 2);
        }
    }

    // Carisma - Movimientos Hipnotizantes 2d4+car
    void Hipnotizar()
    {
        Debug.Log("Movimientos Hipnotizantes de slime");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Carisma(4, 2);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Carisma(4, 4);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
}
