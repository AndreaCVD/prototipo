using UnityEngine;

public class CommandManager : MonoBehaviour
{
    [SerializeField] TurnRoundManager turnRoundManager;

    public void Fuerza()
    {
        turnRoundManager.current.Fuerza();
    }
}
