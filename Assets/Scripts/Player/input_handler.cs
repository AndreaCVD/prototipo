using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Mov normal")]
    public Vector3 moveVector;  // Store the horizontal and vertical input as a Vector3
    public bool Jump;  // To track if the player presses the jump key
    CharacterInteract characterInteract;

    player_movement characterMovement;

    [Header("Escena pausada")]
    public bool scenePaused;
    private string horizontalInput = "Horizontal";
    private string verticalInput = "Vertical";
    //private KeyCode jumpKey = KeyCode.Space;

    private void Start()
    {
        scenePaused = false;
        characterMovement = GetComponent<player_movement>();
        characterInteract = GetComponent<CharacterInteract>();
    }

    void Update()
    {
        //si la escena no esta pausada
        if ( scenePaused == false) 
        {
            // Get input for horizontal and vertical axes (e.g., A/D, W/S, Arrow keys, or joystick axes)
            moveVector.x = Input.GetAxis(horizontalInput);
            moveVector.y = Input.GetAxis(verticalInput);

            //characterMovement.AddMoveVectorInput(moveVector);

            //Salto
            Jump = Input.GetKeyDown(KeyCode.Space);
            //Mirar si barra espaciadora esta activada
            if (Jump)
            {
                Debug.Log("Spacebar Pressed");
            }

        }
        else
        {
            moveVector.x = 0;
            moveVector.y = 0;
        }
    }
    public void ScenePause(bool newState)
    {
        scenePaused = newState;
    }
}