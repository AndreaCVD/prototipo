using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [SerializeField] CombatMonster playerPersonaje;
    [SerializeField] CombatMonster enemyPersonaje;
    public void StartBattle(Parameters player, Parameters enemy)
    {
        playerPersonaje.Init(player);
        enemyPersonaje.Init(enemy);
    }
}
