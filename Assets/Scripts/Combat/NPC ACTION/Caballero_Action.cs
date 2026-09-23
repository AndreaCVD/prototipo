using UnityEngine;

public class Caballero_Action : MonoBehaviour
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
            commandManager.Caballero_img("idle_prota");
            player_inmovilizado = false;
            ataque = Random.Range(0, 9); //sin atrapar
        }
        else
        {
            //del 0 a 8
            ataque = Random.Range(0, 10);
        }
        // Espada = 50%
        // Golpe = 40%
        // Intimidar = 10%
        switch (Random.Range(8, 10))
        {
            case 0:
                Espada();
                break;
            case 1:
                Espada();
                break;
            case 2:
                Espada();
                break;
            case 3:
                Espada();
                break;
            case 4:
                Espada();
                break;
            case 5:
                Golpe();
                break;
            case 6:
                Golpe();
                break;
            case 7:
                Golpe();
                break;
            case 8:
                Golpe();
                break;
            case 9:
                Intimidar();
                break;
            default:
                Debug.Log("Error de lectura");
                break;
        }
    }
    // Fuerza - 1d12+fue
    void Espada()
    {
        Debug.Log("Espada de caballero");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Caballero_img("ataque");

            commandManager.Fuerza(12, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            commandManager.Caballero_img("ataque");

            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(12, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            commandManager.Change_img("autoataque");

            Debug.Log("Tirada fatidica del enemigo");

            commandManager.AutoHerirse(4, 1);
        }
        else // no supera la armadura
        {
            commandManager.NextTurn();
        }
    }
    // Fuerza - 1d6+fue
    void Golpe()
    {
        Debug.Log("Golpe de caballero");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Caballero_img("ataque");

            //Fuerza(int dado_1, int times_1, int dado_2, int times_2)
            commandManager.Fuerza(6, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            commandManager.Caballero_img("ataque");

            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(6, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            commandManager.Change_img("autoataque");

            Debug.Log("Tirada fatidica del enemigo");

            commandManager.AutoHerirse(4, 1);
        }
        else // no supera la armadura
        {
            commandManager.NextTurn();
        }
    }
    // Carisma
    void Intimidar()
    {

        Debug.Log("Intimidar de caballero");
        int ca_player = commandManager.Armadura(0, 20);
        
        if (ca_player == 2 || ca_player == 0) //supera armadura con o sin critico
        {
            player_inmovilizado = true;
            commandManager.PlayerInmovilizado(true, 1);

            commandManager.Caballero_img("intimidar");
            commandManager.NextTurn();
        }
        else
        {
            commandManager.NextTurn();
        }
    }
}
