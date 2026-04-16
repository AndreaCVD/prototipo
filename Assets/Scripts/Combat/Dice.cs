
using UnityEngine;
public class Dice : MonoBehaviour
{
    

    public int RollDice(int maxValue)
    {
        int a = Random.Range(1, maxValue);
        Debug.Log("Dice=" + a);
        return a;
        
    }
}
