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
    [SerializeField] float walkSpeed = 3f;
    public float rotationSpeed;
    public float jumpForce;
    private float x, y;

    [Header("Anim y Mov")]
    public LayerMask groundLayer;
    private InputHandler _inputHandler;
    private Animator _animator;
    [SerializeField] private Transform modelTransform;
    Rigidbody rb;
    private bool isGrounded;
    private int lastMovement;

    public void AddMoveVectorInput(Vector3 moveVector)
    {
        moveVectorInput = moveVector;
    }

    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _inputHandler = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        jumpForce = 4f;
        rotationSpeed = 220f;
    }

    void FixedUpdate()
    {
        x = _inputHandler.moveVector.x;
        y = _inputHandler.moveVector.y;

        // Animator speed — max 1.25, 0 exacto sin input
        float rawSpeed = new Vector2(x, y).magnitude;
        rawSpeed = rawSpeed < 0.1f ? 0f : Mathf.Clamp(rawSpeed, 0f, 1.25f);
        _animator.SetFloat("Speed", rawSpeed, 0.1f, Time.deltaTime);

        isGrounded = CheckGrounded();

        if (isGrounded && _inputHandler.Jump)
            Jump();

        if (!isGrounded && rb.linearVelocity.y < 0)
            rb.AddForce(Vector3.down * (jumpForce + 9.81f), ForceMode.Acceleration);
        // Last movement [ 0 = X ] [ 1 = Y ]
        if (x == 0 && y != 0)
        { lastMovement = 0;
            MoveCharacter(x, y);
        }
        else if (x != 0 && y == 0)
        { lastMovement = 1;
            MoveCharacter(x, y);
        }
        if (x != 0 && y != 0) //clicar las dos a la vez
        { 
            if (lastMovement == 0)
                MoveCharacter(x, 0);
            else
                MoveCharacter(0, y);
        }
    }

    private void MoveCharacter(float horizontalInput, float verticalInput)
    {
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        rb.MovePosition(rb.position + moveDirection * walkSpeed * Time.deltaTime);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
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

    /*

    private void RotateCharacter(float y)
    {
        //float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;

        //Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        rb.rotation = Quaternion.Euler(0, y, 0);
        //rb.MoveRotation(deltaRotation);
    }
    */

    /*
    private void MoveCharacter(float verticalInput)
    {
   
        Vector3 moveDirection = Vector3.forward * y; //Atras y delante

        //Tenemos que mover al personaje en el Eje del Mundo, no del jugador
        rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
        if (verticalInput>0)//positivo
        {
            RotateCharacter(0f); //D
            //Debug.Log("DELANTE");
        }
        else //negativo
        {
            RotateCharacter(180f); //A
            //Debug.Log("ATRAS");
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
            //Debug.Log("DERECHA");
        }
        else //negativo
        {
            RotateCharacter(270f); //S
            //Debug.Log("IZQUIERDA");
        }
        // RotateCharacter(horizontalInput);
    }

    */
}