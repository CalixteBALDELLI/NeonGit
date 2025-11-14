using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public           InputActionReference      move;  // reference à l'action move
    public           InputActionReference      fire;
    public           Rigidbody2D               rb; //reference au rigidbody
    public           CharacterScriptableObject characterData;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] bool logValues;
    
    [HideInInspector]
    public Vector2 moveDirection; // moving direction
    [HideInInspector]
    public Vector2 lastMovedVector;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    
    [Header("Footsteps Settings")]
    public AudioSource footstepSource;
    public  AudioClip[] footstepClips;
    public  float       stepInterval  = 0.35f; // Temps entre les pas (à ajuster)
    private float       footstepTimer = 0f;
    
    GameObject player;
    void Start()
    {
        player = gameObject;
        DontDestroyOnLoad(player);
        lastMovedVector = new Vector2(1, 0f);
    }
    private void Update() //Direction Checker
    {
        if (logValues)
        {
            Debug.Log("Last Moved Vector = " + lastMovedVector + " Move Direction = " + moveDirection);
        }
        
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
        
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                if (footstepClips.Length > 0)
                {
                    int index = UnityEngine.Random.Range(0, footstepClips.Length);
                    footstepSource.PlayOneShot(footstepClips[index]);
                }

                footstepTimer = stepInterval;  // Reset timer
            }
        }
        else
        {
            // Reset pour éviter les rafales quand on recommence à marcher
            footstepTimer = 0f;
        }
        
    }
    
    


    private void FixedUpdate()
    {
        rb.linearVelocity = new  Vector2(moveDirection.x, moveDirection.y) * playerStats.currentMoveSpeed; // deplacement du personnage sur la carte (mulitpipplication du vecteur de direction par la  valeur de vitesse réglable) 
    }
    
    

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