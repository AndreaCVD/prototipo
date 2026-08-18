using UnityEngine;

public class Libro_Lerendur : MonoBehaviour
{
    int ataque;
    bool player_inmovilizado, escudo, proyectil, ola_atronadora, esfera_llamas;
    [SerializeField] CommandManager commandManager;

    private void Start()
    {
        player_inmovilizado = false;
        escudo = false;
    }

    public void Ataque_Aleatorio()
    {
        //ataques 1 vez * turno: inmovilizar, escudo, proyectil magico, ola atronadora
        //ataque una vez por partida: esfera de llamas
        if (esfera_llamas)
        {
            if (proyectil)
            {
                proyectil = false;
                ataque = Random.Range(4, 12); //sin esfera llamas y proyectil
            }
            ataque = Random.Range(0, 12); //sin esfera llamas

        }
        else
        {
            if (proyectil)
            {
                proyectil = false;
                ataque = Random.Range(4, 14); //con esfera llamas y sin proyectil
            }
            ataque = Random.Range(0, 14); //Con esfera de llamas
        }
        if (player_inmovilizado && ataque == 11)
        {
            if (!commandManager.Return_Inmovil("player"))
                player_inmovilizado = false;
            ataque--; //ahora es un escudo
        }
        // solo uno de estos estara bloqueado un turno, no se solapan
        if (escudo && ataque == 10)
        {
            escudo = false;
            ataque--; //ahora es Ola Atronadora
        }
        if (ola_atronadora && ataque == 7 || ataque == 8 || ataque == 9)
        {
            ola_atronadora = false;
            ataque--;
        }
        else
        {
            //del 0 a 9
            ataque = Random.Range(0, 10);
            if (ataque == 8)
                ataque++;
        }
        Choise();
    }
    // --- SWITCH DECIDIR ATAQUE ---
    void Choise()
    {
        switch (ataque)
        {
        // Proyectil 25%
            case 0:
                Proyectil();
                break;
            case 1:
                Proyectil();
                break;
            case 2:
                Proyectil();
                break;
            case 3:
                Proyectil();
                break;
        // Escarcha 20%
            case 4:
                Escarcha(); 
                break;
            case 5:
                Escarcha(); 
                break;
            case 6:
                Escarcha(); 
                break;
        // Ola Atronadora 20%
            case 7:
                Ola_Atronadora();
                break;
            case 8:
                Ola_Atronadora();
                break;
            case 9:
                Ola_Atronadora();
                break;
        // Escudo 10%
            case 10:
                Escudo(); // 10% 
                break;
        // Inmovilizar 10%
            case 11:
                Inmovilizar(); // 10%
                break;
        // Esfera Llamas 15% --> una vez por partida
            case 12:
                Esfera_Llamas();
                break;
            case 13:
                Esfera_Llamas();
                break;
            default:
                Debug.Log("Error de lectura");
                break;
        }
    }
    // --- ATAQUES ---
    // Inteligencia - inmovilizar 2 turnos
    void Inmovilizar()
    {
        Debug.Log("Inmovilizar de jefe");
        player_inmovilizado = true;
        commandManager.PlayerInmovilizado(true, 2);
        commandManager.NextTurn();
    }
    // Inteligencia - Escudo +5CA (durante 1 turno)
    void Escudo()
    {
        Debug.Log("Escudo de jefe");
        escudo = true;
        commandManager.Modificar_CA(5); //modificar salta turno solo
    }
    // Inteligencia - Rayo Escarcha 1d8
    void Escarcha()
    {
        Debug.Log("Rayo escarcha de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Inteligencia(8, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Inteligencia(8, 1);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
    // Inteligencia - Proyectil Magico 3d4+1, el siguiente turno no lo usa
    void Proyectil()
    {
        proyectil = true;
        Debug.Log("Proyectil Magico de jefe");
        //comprovar 3 veces a la CA, si uno llega le da solo ese
        int proyectiles = 0;
        for (int i = 0; i > 3; i++)
        {
            int ca_player = commandManager.Armadura(0, 20);
            if (ca_player == 2 || ca_player == 0) //supera armadura
            {
                proyectiles++;
            }
        }
        if (proyectiles != 0)
            commandManager.Fuerza(4, proyectiles);
        else 
            commandManager.NextTurn();
    }
    // Inteligencia - Ola Atronadora 2d8, el siguiente turno no lo usa
    void Ola_Atronadora()
    {
        ola_atronadora = true;
        Debug.Log("Ola Atronadora de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Inteligencia(8, 2);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Inteligencia(8, 4);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
    // Inteligencia - Esfera de llamas 3d6, una vez por combate
    void Esfera_Llamas()
    {
        esfera_llamas = true;
        Debug.Log("Esfera Llamas de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Inteligencia(6, 3);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Inteligencia(6, 6);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
}
