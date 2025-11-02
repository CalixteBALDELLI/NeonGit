using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    
    public float moveSpeed; //vitesse de déplacement
    
    public Vector2 moveDirection; // moving direction
    public InputActionReference move;  // reference à l'action move
    public InputActionReference fire;
    [HideInInspector]
    public Vector2 lastMovedVector;
    
    public float lastHorizontalVector;
    public float lastVerticalVector;
    public Rigidbody2D rb; //reference au rigidbody
    public CharacterScriptableObject characterData;

    GameObject player;
    void Start()
    {
        player = gameObject;
        DontDestroyOnLoad(player);
        lastMovedVector = new Vector2(1, 0f);
    }
    private void Update() //Direction Checker
    {
        moveDirection = move.action.ReadValue<Vector2>(); // détection des input, transfert du résultat vers la variable de direction
        
        if (moveDirection.x != 0 && moveDirection.y == 0 && lastVerticalVector != 0) // Si déplacement de l'avatar horizontal
        {
            lastVerticalVector = 0;
        }

        if (moveDirection.x == 0 && moveDirection.y != 0 && lastHorizontalVector != 0) //Si déplacement de l'avatar vertical
        {
            lastHorizontalVector = 0;
        }
        
        
        
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
        rb.linearVelocity = new  Vector2(moveDirection.x, moveDirection.y) * characterData.MovingSpeed; // deplacement du personnage sur la carte (mulitpipplication du vecteur de direction par la  valeur de vitesse réglable) 
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