using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D          rb;
    Vector2                               mouseDirection;
    Vector2                               moveDirection;
    Vector2                               mousePosition;
    [SerializeField] InputActionReference mouseMovement;
    Camera                                playerCamera;
    [SerializeField] float                speed;
    [SerializeField] int                  currentPierce;

    void Start()
    {
        speed         = ModuleManager.SINGLETON.modulesData[ModuleManager.SINGLETON.projectileAcquired].Speed;
        currentPierce = ModuleManager.SINGLETON.modulesData[ModuleManager.SINGLETON.projectileAcquired].Pierce;
        playerCamera  = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        mousePosition = mouseMovement.action.ReadValue<Vector2>();
        Vector3 worldPosition = playerCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, playerCamera.nearClipPlane));
        mouseDirection = (worldPosition - transform.position).normalized;
        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        moveDirection      = new Vector2(Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
        Destroy(gameObject, 5f);
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDirection.y, moveDirection.x) * speed;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy once the pierce peaches 0
        
        currentPierce--;
        if (currentPierce <= 0)
        {
            Destroy(gameObject);
        }
    }
}
