using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    Vector3 moveVectorInput;
    Vector3 moveDirection;
    [Header("Variables")]
    [SerializeField] float speed = 10f;
    public float rotationSpeed;
    public float jumpForce; //Cambiar este en mov
    private float x, y;

    [Header("Anim y Mov")]
    public LayerMask groundLayer;
    private InputHandler _inputHandler;
    Rigidbody rb;
    private bool isGrounded;

    public void AddMoveVectorInput(Vector3 moveVector)
    {
        moveVectorInput = moveVector;

    }
    void Start()
    {
        // Obtener la referencia del InputHandler
        _inputHandler = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        jumpForce = 4f;
        //runSpeed = 5f;
        rotationSpeed = 220f;
    }

    //movemos al personaje
    void Update()
    {
        //HandleMovement();
        // Obtener los valores de movimiento desde InputHandler
        x = _inputHandler.moveVector.x;  // Horizontal (movimiento de izquierda/derecha)
        y = _inputHandler.moveVector.y;  // Vertical (movimiento hacia adelante/atr�s)

        isGrounded = CheckGrounded();

        if (isGrounded && _inputHandler.Jump)
        {
            Jump();
        }

        // Detectar si el jugador está en el suelo y aplicar la gravedad adicional si no lo está
        if (!isGrounded && rb.linearVelocity.y < 0)  // Si el jugador está cayendo
        {
            rb.AddForce(Vector3.down * (jumpForce + 9.81f), ForceMode.Acceleration);
        }

        if (x != 0 || y != 0)
        {
            RotateCharacter(x);
            RotateCharacter(y);
        }

        if (x != 0 || y != 0)
        {
            MoveCharacter(x);
            MoveCharacter(y);
        }
    }

    private void HandleMovement()
    {
        //Calcular direccion dnd se mueve el personaje
        moveDirection = moveVectorInput;

        Vector3 moveVelocity = moveDirection * speed;
        moveVelocity += Physics.gravity;

        rb.linearVelocity = moveVelocity;
    }


    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool CheckGrounded()
    {

        Collider _collider = GetComponent<Collider>();

        Vector3 rayOrigin = _collider.bounds.center - Vector3.up * 0.5f;

        bool grounded = Physics.Raycast(rayOrigin, Vector3.down, 1f, groundLayer);

        Debug.DrawRay(rayOrigin, Vector3.down * 1f, grounded ? Color.green : Color.red);
        return grounded;
    }

    private void RotateCharacter(float horizontalInput)
    {
        float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;

        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);

        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    private void MoveCharacter(float verticalInput)
    {
        Vector3 moveDirection = transform.forward * y;

        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
    }

}