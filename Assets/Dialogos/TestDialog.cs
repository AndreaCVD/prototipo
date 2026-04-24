using UnityEngine;

public class TestDialog : MonoBehaviour
{
    [SerializeField]  DialogBehaviour dialogBehaviour;
    [SerializeField]  DialogNodeGraph dialogGraph;

    private void Start()
    {
        dialogBehaviour.StartDialog(dialogGraph);
    }
}
