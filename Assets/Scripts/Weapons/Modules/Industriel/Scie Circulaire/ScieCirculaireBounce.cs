using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class ScieCirculaireBounce : MonoBehaviour
{
    public float speed = 5f;
    public InputActionReference mousePositionInput;
    public int maxBounces = 3;

    private Vector3 direction;
    private Camera playerCamera;
    private int bounceCount = 0;

    void Start()
    {
        playerCamera = Camera.main;

        // Get mouse position in world
        Vector2 mouseScreenPosition = mousePositionInput.action.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = playerCamera.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, playerCamera.nearClipPlane));
        mouseWorldPosition.z = 0f;

        direction = (mouseWorldPosition - transform.position).normalized;

        StartCoroutine(MoveObject());
    }

    IEnumerator MoveObject()
    {
        while (true)
        {
            transform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (bounceCount >= maxBounces)
        {
            Destroy(gameObject);
            return;
        }

        bounceCount++;

        // Get the normal of the collision surface
        Vector2 normal = collision.contacts[0].normal;

        // Reflect the current direction using the normal
        direction = Vector3.Reflect(direction, normal).normalized;
    }
}