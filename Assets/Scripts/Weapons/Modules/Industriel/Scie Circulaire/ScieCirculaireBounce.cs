using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class ScieCirculaireBounce : MonoBehaviour
{
    public float                  speed = 5f;
    public InputActionReference   mousePositionInput;
    public int                    maxBounces = 3;
    public WeaponScriptableObject circulaireData;

    private Vector3 direction;
    private Camera playerCamera;
    private int bounceCount = 0;

    void Start()
    {
        playerCamera = Camera.main;

        // Get mouse position in world
        //Debug.Log("Rebond Speed : " + ModuleManager.SINGLETON.modulesData[23 + ModuleManager.SINGLETON.rebondAcquired].Speed);
        //Debug.Log("Rebond Pierce : " + ModuleManager.SINGLETON.modulesData[23 + ModuleManager.SINGLETON.rebondAcquired].Pierce);
        Vector2 mouseScreenPosition = mousePositionInput.action.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = playerCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, playerCamera.nearClipPlane));
        mouseWorldPosition.z = 0f;

        direction = (mouseWorldPosition - transform.position).normalized;

        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (ModuleManager.SINGLETON.rebondAcquired >= 1)
        {
            transform.Translate(direction * ModuleManager.SINGLETON.modulesData[42 + ModuleManager.SINGLETON.rebondAcquired].Speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounceCount >= ModuleManager.SINGLETON.modulesData[42 + ModuleManager.SINGLETON.rebondAcquired].Pierce)
        {
            Destroy(gameObject);
            return;
        }

        bounceCount++;
        Debug.Log(" Bounce Count : " + bounceCount);
        // Get the normal of the collision surface
        Vector2 normal = collision.contacts[0].normal;

        // Reflect the current direction using the normal
        direction = Vector3.Reflect(direction, normal).normalized;
    }
}