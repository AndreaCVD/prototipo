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
        rb.isKinematic = false;
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

        if (x == 0 && y != 0)
        {
            //RotateCharacter(x);
            //RotateCharacter(y);
            MoveCharacter(y);

        }

        if (x != 0 && y == 0)
        {
            MoveCharacter2(x);
            //RotateCharacter(x);
            //MoveCharacter(y);
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

    private void RotateCharacter(float y)
    {
        //float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;

        //Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        rb.rotation = Quaternion.Euler(0, y, 0);
        //rb.MoveRotation(deltaRotation);
    }

    private void MoveCharacter(float verticalInput)
    {
   
        Vector3 moveDirection = Vector3.forward * y; //Atras y delante

        //Tenemos que mover al personaje en el Eje del Mundo, no del jugador
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        if (verticalInput>0)//positivo
        {
            RotateCharacter(0f); //D
            Debug.Log("DELANTE");
        }
        else //negativo
        {
            RotateCharacter(180f); //A
            Debug.Log("ATRAS");
        }

        //var rotation = Quaternion.LookRotation(rb.direction);
        //rb.MoveRotation(rotation);
    }
    private void MoveCharacter2(float horizontalInput)
    {
        //Vector3 || transform.right  --> el transform es del obj, Vector3 del mundo
        Vector3 moveDirection = Vector3.right * x; //Derecha e izquierda

        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        if (horizontalInput > 0)//positivo
        {
            RotateCharacter(90f); //D
            Debug.Log("DERECHA");
        }
        else //negativo
        {
            RotateCharacter(270f); //S
            Debug.Log("IZQUIERDA");
        }
        // RotateCharacter(horizontalInput);
    }
    //private void Turn(Vector3 dir)
    //{
    //    Vector3 target = transform.position + dir;
    //    target.y = transform.position.y;
    //    transform.LookAt(target);
    //}
}