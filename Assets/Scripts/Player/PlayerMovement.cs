using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; //reference au rigidbody
    
    public float moveSpeed; //vitesse de déplacement
    
    public Vector2 moveDirection; // moving direction

    public InputActionReference move;  // reference à l'action move
    public InputActionReference fire;
    [HideInInspector]
    public Vector2 lastMovedVector;
    public float lastHorizontalVector;
    public float lastVerticalVector;

    void Start()
    {
        lastMovedVector = new Vector2(1, 0f);
    }
    private void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>(); // détection des input, transfert du résultat vers la variable de direction
        if (moveDirection.x != 0)
        {
            lastHorizontalVector = moveDirection.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); // Last moved x
        }

        if (moveDirection.y != 0)
        {
            lastVerticalVector = moveDirection.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); //Last moved Y
        }

        if (moveDirection.x != 0 && moveDirection.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }
    


    private void FixedUpdate()
    {
        rb.linearVelocity = new  Vector2(moveDirection.x, moveDirection.y) * moveSpeed; // deplacement du personnage sur la carte (mulitpipplication du vecteur de direction par la  valeur de vitesse réglable) 
    }
    
    // tir

    private void OnEnable()
    {
        fire.action.started += Fire;
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        Debug.Log("Fire");
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
    }
    
}