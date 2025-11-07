using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float knockbackForce = 10f;

    public Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSword"))
        {
            Vector2 knockbackDirection = (rb.position - (Vector2) other.transform.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            Debug.Log("Player hit by enemy");
        }
    }
}