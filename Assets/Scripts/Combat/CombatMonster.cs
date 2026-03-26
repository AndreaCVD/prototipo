using System.Security.Cryptography;
using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    Parameters player; //Al que le toque recibir daño
    Save_Stats guardado; //Enviar las estats

    public Int2Val HP;
    public int damage;

    public void Init(Parameters player) //Al que le toque atacar
    {
        //inicializamos el jugador
        this.player = player;
        //colocamos copia del modelo
        GameObject modelo = Instantiate(player.modelPrefab, transform);
        //restablecer rotacion
        player.modelPrefab.transform.localPosition = Vector3.zero;
        player.modelPrefab.transform.localRotation = Quaternion.identity;
        //Setear vida
        int contitucion = player.stats.Get(PersonajesStats.Constitucion);
        HP = new Int2Val (contitucion, contitucion);
    }

    public void Fuerza(CombatMonster target) //Enemigo
    {
        //Busca en parameters los stats del personaje anteriormente inicializado
        /*Debug.Log("Personaje: " 
            + personaje.namePers 
            + "Damage: " 
            + personaje.stats.Get(PersonajesStats.Fuerza)
        );*/
        //Este daño al enemigo
        //int constitucion = personaje.stats.Get(PersonajesStats.Constitucion);
        target.TakeDamage(player.stats.Get(PersonajesStats.Fuerza));
   
    }

    public void TakeDamage(int damage)
    {

        HP.current -= damage;
        // a -= damage;
        //enemigo.stats.values[3].value++;
        player.stats.values[3].value -= damage;

        guardado.guardar_stats(player, player.stats.values[3]);
        Debug.Log(player.stats.values[3].value); 

        Debug.Log("Personaje : " + player.namePers + "HP : " + HP.current.ToString());

        if (HP.current <= 0)
        {
            HP.current = 0;
            Debug.Log("Derrotado : " + player.namePers);
        }
    }
}
