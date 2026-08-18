using UnityEngine;

public class Jefe_Action : MonoBehaviour
{
    int ataque;
    bool player_inmovilizado, convencer;
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
            ataque = Random.Range(3, 10); //sin atrapar
        }
        else if (convencer)
        {
            convencer = false;
            ataque = Random.Range(0, 8); // sin convencer
        }
        else
        {
            //del 0 a 9
            ataque = Random.Range(0, 10); // con atrapar

        }
        Choise();

    }
    // --- SWITCH DECIDIR ATAQUE ---
    void Choise()
    {
        switch (ataque)
        {
        // Atrapar 30%
            case 0:
                Atrapar();
                break;
            case 1:
                Atrapar();
                break;
            case 2:
                Atrapar();
                break;
        // Ataque X 30% 
            case 3:
                Ataque_X();
                break;
            case 4:
                Ataque_X();
                break;
            case 5:
                Ataque_X();
                break;
        // Libretazo 10%
            case 6:
                Libretazo();
                break;
        // Corte de página 10%
            case 7:
                Corte();
                break;

            // Convencer 20%    
            case 8:
                Convencer();
                break;
            case 9:
                Convencer();
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
        player_inmovilizado = true;
        commandManager.PlayerInmovilizado(true, 1);
        commandManager.NextTurn();
    }
    // Carisma - inmovilizado 1 turno y 1d4+carisma
    void Convencer()
    {
        Debug.Log("Convencer de jefe");
        player_inmovilizado = true;
        commandManager.PlayerInmovilizado(true, 1);
        Debug.Log("EL LIBRO TE ESTA INTENTANDO CONVENCER DE UNIRTE A EL");
        Debug.Log("ACTIVAR DIALOGO");
        commandManager.NextTurn();
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
