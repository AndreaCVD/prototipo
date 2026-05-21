
using UnityEngine;
using UnityEditor;

public class Save_Stats : MonoBehaviour
{
    [Header("Ficha personaje")]
    //Mover los stats de los personajes del combate a la pantalla principal
    [SerializeField] Parameters playerPersonaje;
    [SerializeField] Parameters slime;
    [SerializeField] Parameters caballero;
    [SerializeField] Parameters libro;
    //[SerializeField] ValueContainer constitucion_prota;

    [Header("Personaje")]
    public int vida_prota;
    public int vida_protaCambio;

    //1. Declaro la variable para el objeto Game Manager
    private GameObject gameManager;

    private void Awake()
    {
        //Setear los valores para cada vez
        //Player
        playerPersonaje.stats.values[0].value = 5; //Fuerza
        playerPersonaje.stats.values[1].value = 3; //Inteligencia
        playerPersonaje.stats.values[2].value = 3; //Carisma
        playerPersonaje.stats.values[3].value = 50; //Vida
        playerPersonaje.stats.values[4].value = 14; //Armadura
        //Player Stats
        playerPersonaje.Inventario.Llave.Clear();
        playerPersonaje.Inventario.LlaveMaestra.Clear();
        playerPersonaje.Inventario.Daga.Clear();
        playerPersonaje.Inventario.Espada.Clear();
        playerPersonaje.Inventario.PocionVida.Clear();

        // playerPersonaje.Inventario.Clear(); //Vaciamos el Inventario

        ////Slime
        //slime.stats.values[0].value = 5; //Fuerza
        //slime.stats.values[1].value = 3; //Inteligencia
        //slime.stats.values[2].value = 3; //Carisma
        //slime.stats.values[3].value = 50; //Vida
        //slime.stats.values[4].value = 14; //Armadura

        ////Cabellero
        //caballero.stats.values[0].value = 5; //Fuerza
        //caballero.stats.values[1].value = 3; //Inteligencia
        //caballero.stats.values[2].value = 3; //Carisma
        //caballero.stats.values[3].value = 50; //Vida
        //caballero.stats.values[4].value = 14; //Armadura
        //se guarda?
        ////Libro
        //libro.stats.values[0].value = 5; //Fuerza
        //libro.stats.values[1].value = 3; //Inteligencia
        //libro.stats.values[2].value = 3; //Carisma
        //libro.stats.values[3].value = 50; //Vida
        //libro.stats.values[4].value = 14; //Armadura

        //Empezar el inventario
        //playerPersonaje.Inventario.Add(playerPersonaje.llave);
        //playerPersonaje.Inventario.Add(playerPersonaje.llave_maestra);
        //playerPersonaje.Inventario.Add(playerPersonaje.pocion_vida);
        //playerPersonaje.Inventario.Add(playerPersonaje.daga);
        //playerPersonaje.Inventario.Add(playerPersonaje.espada);
    }
    void Start()
    {
        int vida_prota = playerPersonaje.stats.Get(PersonajesStats.Constitucion);
        //2. Busco el objeto GameManager en la escena y lo asocio a la variable
        gameManager = GameObject.Find("--SceneManagement--");

        //3. Le indico que no se destruya entre escenas
        DontDestroyOnLoad(gameManager);

        //Llegar a los valores: protagonista es parameters
            //protagonista.stats.Get(PersonajesStats.Carisma);
    }
    void Update()
    {
        if ( vida_prota != vida_protaCambio)
        {
            playerPersonaje.stats.values[3].value -= vida_protaCambio;
            vida_prota = vida_protaCambio;
        }
    }

    public void guardar_stats( Parameters player, int damage)
    { 
        //ver si es el prota o no
        if ( player == playerPersonaje)
        {
            player.stats.values[3].value -= damage;
            vida_protaCambio = player.stats.Get(PersonajesStats.Constitucion);
        }

    }

    public void alguien_eliminado(Parameters player)
    {
        if (player == playerPersonaje)//si el prota se ha quedado sin vida 
        {
            //--> fin juego
        }
        else //el enemigo ha muerto
        {
            //tenemos que destruirlo de la escena
        }
    }


}
