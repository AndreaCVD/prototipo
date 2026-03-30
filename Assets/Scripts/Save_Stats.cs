using UnityEngine;

public class Save_Stats : MonoBehaviour
{
    //Mover los stats de los personajes del combate a la pantalla principal
    [SerializeField] Parameters playerPersonaje;
    //[SerializeField] ValueContainer constitucion_prota;

    //Valores
    public int vida_prota;
    public int vida_protaCambio;

    //1. Declaro la variable para el objeto Game Manager
    private GameObject gameManager;

    void Start()
    {
        int vida_prota = playerPersonaje.stats.Get(PersonajesStats.Constitucion);
        //2. Busco el objeto GameManager en la escena y lo asocio a la variable
        gameManager = GameObject.Find("--SceneManagement--");

        //3. Le indico que no se destruya entre escenas
        DontDestroyOnLoad(gameManager);

        //Llegar a los valores: (ptoyagonista es parameters
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
        player.stats.values[3].value -= damage;
        vida_protaCambio = player.stats.Get(PersonajesStats.Constitucion);
    }

}
