using UnityEngine;

public class CombatMonster : MonoBehaviour
{
    public void Init( PersonajesStats player)
    {
        GameObject modelo = Instantiate(player.modelPrefab, transform);
    }
}
