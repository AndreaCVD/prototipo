
using System.Security.Cryptography;
using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    Parameters personaje;

    public void Init(Parameters player)
    {
       GameObject modelo = Instantiate(player.modelPrefab, transform);
        //restablecer rotacion
        player.modelPrefab.transform.localPosition = Vector3.zero;
        player.modelPrefab.transform.localRotation = Quaternion.identity;
    }

    public void Fuerza()
    {
        Debug.Log("Personaje: " 
            + personaje.namePers 
            + "Damage?: " 
            + personaje.stats.Get(PersonajesStats.Fuerza));
    }
}
