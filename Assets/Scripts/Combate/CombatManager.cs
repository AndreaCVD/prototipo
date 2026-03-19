using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] CombatMonster playerPersonaje;
    [SerializeField] CombatMonster enemyPersonaje;
    public void StartBattle(PersonajesStats player, PersonajesStats enemy)
    {
        playerPersonaje.Init(player);
        enemyPersonaje.Init(enemy);
    }
}
