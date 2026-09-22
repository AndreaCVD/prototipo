using UnityEngine;

public class Jefe_Action : MonoBehaviour
{
    [Header("EL DIALOGO DEL OBJ")]
    public Dialog dialog;
    [SerializeField] cherrydev.DialogNodeGraph dialogo_libro;

    int ataque;
    bool player_inmovilizado, convencer;
    [SerializeField] CommandManager commandManager;

    private void Start()
    {
        player_inmovilizado = false;

        if (dialog == null)
            dialog = GameObject.Find("--CombatDialog--").GetComponent<Dialog>();
        
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
        Convencer();
        //switch (ataque)
        //{
        //// Atrapar 30%
        //    case 0:
        //        Atrapar();
        //        break;
        //    case 1:
        //        Atrapar();
        //        break;
        //    case 2:
        //        Atrapar();
        //        break;
        //// Ataque X 30% 
        //    case 3:
        //        Ataque_X();
        //        break;
        //    case 4:
        //        Ataque_X();
        //        break;
        //    case 5:
        //        Ataque_X();
        //        break;
        //// Libretazo 10%
        //    case 6:
        //        Libretazo();
        //        break;
        //// Corte de página 10%
        //    case 7:
        //        Corte();
        //        break;

        //    // Convencer 20%    
        //    case 8:
        //        Convencer();
        //        break;
        //    case 9:
        //        Convencer();
        //        break;

        //    default:
        //        Debug.Log("Error de lectura");
        //        break;
        //}
    }
    // Fuerza - 1d6+fue
    void Libretazo()
    {
        Debug.Log("Libretazo de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Libro_img("libretazo");

            commandManager.Fuerza(6, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            commandManager.Libro_img("libretazo");

            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(6, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            commandManager.model_dados(4);

            commandManager.Change_img("autoataque");

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
            commandManager.Libro_img("corte");

            commandManager.Fuerza(4, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            commandManager.Libro_img("corte");

            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(4, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            commandManager.Change_img("autoataque");

            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }
    // Inteligencia - durante 1 turno
    void Atrapar()
    {
        Debug.Log("Atrapar de jefe");
        player_inmovilizado = true;

        commandManager.Libro_img("atrapar");

        commandManager.PlayerInmovilizado(true, 1);
        commandManager.NextTurn();
    }
    // Carisma - inmovilizado 1 turno y 1d4+carisma
    void Convencer()
    {
        player_inmovilizado = true;
        commandManager.PlayerInmovilizado(true, 1);

        commandManager.Libro_img("convencer");

        Debug.Log("EL LIBRO TE ESTA INTENTANDO CONVENCER DE UNIRTE A EL");

        dialog.EmpezarDialogo(dialogo_libro, this.gameObject);
        //commandManager.NextTurn();
    }
    public void Aceptar()
    {
        GameObject aux = GameObject.Find("--SceneManagement--");
        LoadScene load = aux.GetComponent<LoadScene>();
        Preload preload = aux.GetComponent<Preload>();
        //salir de combate
        //activar animacion de que el libro se va con Carlos
        //la pantalla oscurece -> Game Over
        Debug.Log("ACABAR COMBATE");

        preload.Carlos_Death();
        load.SalirCombate();
    }
    public void Denegar()
    {
        commandManager.NextTurn();
    }

    // Inteligencia - 2d6+intel
    void Ataque_X()
    {
        Debug.Log("Ataque X de jefe");
        int ca_player = commandManager.Armadura(0, 20);

        if (ca_player == 2) //supera armadura
        {
            commandManager.Libro_img("x");

            commandManager.Inteligencia(6, 2);
        }
        else if (ca_player == 0) //CRITICO
        {
            commandManager.Libro_img("x");

            Debug.Log("Tirada critica del enemigo");
            commandManager.Inteligencia(6, 4);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            commandManager.Change_img("autoataque");

            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
    }

}
