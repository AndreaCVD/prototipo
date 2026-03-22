using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Mov normal")]
    public Vector3 MoveInput;  // Store the horizontal and vertical input as a Vector3
    public bool Jump;  // To track if the player presses the jump key


    private string horizontalInput = "Horizontal";
    private string verticalInput = "Vertical";
    //private KeyCode jumpKey = KeyCode.Space;

    private void Start()
    {    }

    void Update()
    {
        // Get input for horizontal and vertical axes (e.g., A/D, W/S, Arrow keys, or joystick axes)
        MoveInput.x = Input.GetAxis(horizontalInput);
        MoveInput.y = Input.GetAxis(verticalInput);

        //Salto
        Jump = Input.GetKeyDown(KeyCode.Space);
        //Mirar si barra espaciadora esta activada
        if (Jump)
        {
            Debug.Log("Spacebar Pressed");
        }
    }
}