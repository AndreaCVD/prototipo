using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CombatMonster : MonoBehaviour
{
    Parameters player; //Al que le toque recibir daño
    Save_Stats guardado; //Enviar las estats
    [SerializeField] Image imagenPers; 
    public Int2Val HP;
    public int damage;

    public void Init(Parameters player) //Al que le toque atacar
    {
        //inicializamos el jugador
        this.player = player;
            //colocamos copia del modelo
            //GameObject modelo = Instantiate(player.modelPrefab, transform);
        //colocamos imagen
        imagenPers.sprite = player.art;
            //restablecer rotacion
            //player.modelPrefab.transform.localPosition = Vector3.zero;
            //player.modelPrefab.transform.localRotation = Quaternion.identity;
        //Setear vida
        int contitucion = player.stats.Get(PersonajesStats.Constitucion);
        HP = new Int2Val (contitucion, contitucion);
    }

    public void Fuerza(CombatMonster target) //Enemigo
    {
        //Este daño al enemigo
        //int constitucion = personaje.stats.Get(PersonajesStats.Constitucion);
        target.TakeDamage(player.stats.Get(PersonajesStats.Fuerza)); //el target recibe daño de la fuerza
    }
    public void Inteligencia(CombatMonster target) //Enemigo
    {
        //Este daño al enemigo
        //int constitucion = personaje.stats.Get(PersonajesStats.Constitucion);
        target.TakeDamage(player.stats.Get(PersonajesStats.Inteligencia)); //el target recibe daño de la fuerza
    }
    public void Carisma(CombatMonster target) //Enemigo
    {
        //Este daño al enemigo
        //int constitucion = personaje.stats.Get(PersonajesStats.Constitucion);
        target.TakeDamage(player.stats.Get(PersonajesStats.Carisma)); //el target recibe daño de la fuerza
    }

    public void TakeDamage(int damage)
    {

        HP.current -= damage;
        // a -= damage;
        //enemigo.stats.values[3].value++;
        player.stats.values[3].value -= damage;

        //guardado.guardar_stats(player, damage); //guardar estats
        //Debug.Log(player.stats.values[3].value); 

        Debug.Log(player.namePers + " ha sido atacado! : "+ "// HP : " + HP.current.ToString());

        if (HP.current <= 0)
        {
            HP.current = 0;
            Debug.Log("Derrotado : " + player.namePers);
            //guardado.alguien_eliminado(player); //enviara el personaje que se elimine
        }
    }
}
