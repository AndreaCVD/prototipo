using UnityEngine;

public class Mimic_Action : MonoBehaviour
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
        // Mordisco = 40%
        // Vomito = 30%
        // Lenguetazo = 20%
        // Atrapar = 10%    
        switch (ataque)
        {
            case 0:
                Mordisco(); 
                break;
            case 1:
                Vomito(); 
                break;
            case 2:
                Mordisco(); 
                break;
            case 3:
                Vomito(); 
                break;
            case 4:
                Mordisco(); 
                break;
            case 5:
                Vomito(); 
                break;
            case 6:
                Mordisco();
                break;
            case 7:
                Lenguetazo(); 
                break;
            case 8:
                Lenguetazo(); 
                break;
            case 9:
                Atrapar();
                break;

            default:
                Debug.Log("Error de lectura");
                break;
        }
    }
    // Fuerza - Mordisco 1d8+fue
    void Mordisco()
    {
        Debug.Log("Mordisco de mimic");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            //Fuerza(int dado_1, int times_1, int dado_2, int times_2)
            commandManager.Fuerza(8, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(8, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");

            commandManager.AutoHerirse(4, 1);
        }
        //else // no supera la armadura
        //{
        //    commandManager.NextTurn();
        //}
    }
    // Fuerza - Vomito 1d6+fue+1d4(acido)
    void Vomito()
    {
        Debug.Log("Vomito de mimic");
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
        //else // no supera la armadura
        //{
        //    commandManager.NextTurn();
        //}
    }
    // Inteligencia - Atrapar, inmovilizar 1 turno
    void Atrapar()
    {
        Debug.Log("Atrapar de mimic");
        player_inmovilizado = true;
        commandManager.PlayerInmovilizado(true);
        commandManager.NextTurn();
    }
    // Carisma - lenguetazo, 2d4+carisma
    void Lenguetazo()
    {
        Debug.Log("Lenguetazo de mimic");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            //Fuerza(int dado_1, int times_1, int dado_2, int times_2)
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
        //else // no supera la armadura
        //{
        //    commandManager.NextTurn();
        //}
    }
}
