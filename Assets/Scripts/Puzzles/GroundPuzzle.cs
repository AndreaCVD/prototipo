using UnityEngine;

public class GroundPuzzle : MonoBehaviour
{
    [SerializeField] PuzzleZone puzzleZone;

    void OnCollision (Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Player ha tocado el suelo
            //Devolver al player a la posicion
            puzzleZone.PositionPlayer();


        }
    }
}
