using System.Security.Cryptography;
using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    Parameters personaje;

    public Int2Val HP;
    public int damage;

    public void Init(Parameters player)
    {
        //inicializamos el jugador
        personaje = player;
        //colocamos copia del modelo
        GameObject modelo = Instantiate(player.modelPrefab, transform);
        //restablecer rotacion
        player.modelPrefab.transform.localPosition = Vector3.zero;
        player.modelPrefab.transform.localRotation = Quaternion.identity;
        //Setear vida
        int contitucion = player.stats.Get(PersonajesStats.Constitucion);
        HP = new Int2Val (contitucion, contitucion);
    }

    public void Fuerza(CombatMonster target)
    {
        //Busca en parameters los stats del personaje anteriormente inicializado
        /*Debug.Log("Personaje: " 
            + personaje.namePers 
            + "Damage: " 
            + personaje.stats.Get(PersonajesStats.Fuerza)
        );*/
        //Este daño al enemigo
        target.TakeDamage(personaje.stats.Get(PersonajesStats.Fuerza));
    }

    public void TakeDamage(int damage)
    {
        HP.current -= damage;
        Debug.Log("Personaje : " + personaje.namePers + "HP : " + HP.current.ToString());
        if (HP.current <= 0)
        {
            HP.current = 0;
            Debug.Log("Derrotado : " + personaje.namePers);
        }
    }
}
