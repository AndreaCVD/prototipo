using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public CombatMonster playerPersonaje;
    public CombatMonster enemyPersonaje;
    public void StartBattle(Parameters player, Parameters enemy)
    {
        //Iniciar los personajes
        playerPersonaje.Init(player);
        enemyPersonaje.Init(enemy);
    }


}
