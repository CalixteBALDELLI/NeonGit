using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; //reference au rigidbody
    
    public float moveSpeed; //vitesse de déplacement
    
    public Vector2 moveDirection; // moving direction

    public InputActionReference move;  // reference à l'action move
    public InputActionReference fire;
    
    private void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>(); // détection des input, transfert du résultat vers la variable de direction
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