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
                Puyo();
                break;

            case 8:
                Escudo();
                break;
            case 9:
                Inmovilizar();
                break;
            default:
                Debug.Log("Error de lectura");
                break;
        }
    }

    // Fuerza - 1d4+fue
    void Puyo()
    {
        Debug.Log("Puño de jefe");
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
    // Inteligencia - inmovilizar 2 turnos
    void Inmovilizar()
    {
        Debug.Log("Inmovilizar de jefe");
    }
    // Inteligencia - Escudo +5CA (durante 1 turno)
    void Escudo()
    {
        Debug.Log("Escudo de jefe");
        escudo = true;
        commandManager.Modificar_CA(5);
    }
    // Inteligencia - Rayo Escarcha 1d8
    void Escarcha()
    {
        Debug.Log("Rayo escarcha de jefe");

    }
    // Inteligencia - Proyectil Magico 3d4+1, el siguiente turno no lo usa
    void Proyectil()
    {
        Debug.Log("Proyectil Magico de jefe");
        //comprovar 3 veces a la CA, si uno llega le da solo ese
    }
    // Inteligencia - Ola Atronadora 2d8, el siguiente turno no lo usa
    void Ola_Atronadora()
    {
        Debug.Log("Ola Atronadora de jefe");

    }
    // Inteligencia - Esfera de llamas 3d6, una vez por combate
    void Esfera_Llamas()
    {
        Debug.Log("Esfera Llamas de jefe");

    }
}
