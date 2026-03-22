using System.Security.Cryptography;
using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    Parameters personaje;

    public int HP;
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
        HP -= damage;
        Debug.Log("Personaje : " + personaje.namePers + "HP : " + HP.ToString());
        if (HP <= 0)
        {
            HP = 0;
            Debug.Log("Derrotado : " + personaje.namePers);
        }
    }
}
