using UnityEngine;

public class PuzzleZone : MonoBehaviour
{
    [SerializeField] GameObject FinalPuzzle;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Puzzle")
        {
            Debug.Log("This is a Puzzle");
        }
        Debug.Log(other.tag);
    }
}
