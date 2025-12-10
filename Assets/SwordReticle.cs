using UnityEngine;
using UnityEngine.InputSystem;

public class SwordReticle : MonoBehaviour
{
    [SerializeField] Camera               playerCamera;
    [SerializeField] InputActionReference mouseMovement;
    [SerializeField] Vector2              mousePosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mousePosition = mouseMovement.action.ReadValue<Vector2>();
        Vector3 worldPosition = playerCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, playerCamera.nearClipPlane));
        Vector3 rotation      = (worldPosition - transform.position).normalized;
        float   angle         = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.localEulerAngles =  new Vector3(0, 0, angle);
    }
}
