
using UnityEngine;
using UnityEditor;

public class Save_Stats : MonoBehaviour
{
    [Header("Ficha personaje")]
    //Mover los stats de los personajes del combate a la pantalla principal
    [SerializeField] Parameters playerPersonaje;
    [SerializeField] Parameters slime;
    [SerializeField] Parameters mimic;
    [SerializeField] Parameters caballero;
    [SerializeField] Parameters libro;
    [SerializeField] Parameters yusseif;
    //[SerializeField] ValueContainer constitucion_prota;


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
        //playerPersonaje.Inventario.Llave.Clear();
        //playerPersonaje.Inventario.LlaveMaestra.Clear();
        //playerPersonaje.Inventario.Daga.Clear();
        //playerPersonaje.Inventario.Espada.Clear();
        //playerPersonaje.Inventario.PocionVida.Clear();

        //Los enemigos tienen que hacerse en cada cambio de escena
        ////Slime
        mimic.stats.values[0].value = 3; //Fuerza
        mimic.stats.values[1].value = 0; //Inteligencia
        mimic.stats.values[2].value = 0; //Carisma
        mimic.stats.values[3].value = 20; //Vida
        mimic.stats.values[4].value = 12; //Armadura

        ////Mimic
        slime.stats.values[0].value = 5; //Fuerza
        slime.stats.values[1].value = 0; //Inteligencia
        slime.stats.values[2].value = 0; //Carisma
        slime.stats.values[3].value = 30; //Vida
        slime.stats.values[4].value = 6; //Armadura

        ////Cabellero
        caballero.stats.values[0].value = 2; //Fuerza
        caballero.stats.values[1].value = 0; //Inteligencia
        caballero.stats.values[2].value = 0; //Carisma
        caballero.stats.values[3].value = 16; //Vida
        caballero.stats.values[4].value = 17; //Armadura
  
        ////Libro
        libro.stats.values[0].value = 1; //Fuerza
        libro.stats.values[1].value = 4; //Inteligencia
        libro.stats.values[2].value = 6; //Carisma
        libro.stats.values[3].value = 40; //Vida
        libro.stats.values[4].value = 15; //Armadura

        ////Libro
        yusseif.stats.values[0].value = 3; //Fuerza
        yusseif.stats.values[1].value = 5; //Inteligencia
        yusseif.stats.values[2].value = 2; //Carisma
        yusseif.stats.values[3].value = 60; //Vida
        yusseif.stats.values[4].value = 17; //Armadura

        //Empezar el inventario
        //playerPersonaje.Inventario.Add(playerPersonaje.llave);
        //playerPersonaje.Inventario.Add(playerPersonaje.llave_maestra);
        //playerPersonaje.Inventario.Add(playerPersonaje.pocion_vida);
        //playerPersonaje.Inventario.Add(playerPersonaje.daga);
        //playerPersonaje.Inventario.Add(playerPersonaje.espada);
    }


    public void guardar_stats( Parameters player, int damage)
    { 
        //ver si es el prota o no
        //if ( player == playerPersonaje)
        //{
        //    player.stats.values[3].value -= damage;
        //    vida_protaCambio = player.stats.Get(PersonajesStats.Constitucion);
        //}

    }

    public void restaurarVidaEnemigo()
    {
        ////Slime
        mimic.stats.values[3].value = 20; //Vida

        ////Mimic
        slime.stats.values[3].value = 30; //Vida

        ////Cabellero
        caballero.stats.values[3].value = 16; //Vida

        ////Libro
        libro.stats.values[3].value = 40; //Vida

        ////Yusseif
        yusseif.stats.values[3].value = 60; //Vida
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
