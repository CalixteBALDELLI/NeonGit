using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    private Transform player;
    public bool isAttacked = false;
    public float knockbackForce = 10f;
    public Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttacked == false)
        { 
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime);
        }
        else
        {
            Knockback();   
        }
    }

    void Knockback()
    {
        Vector2 knockbackDirection = (rb.position - (Vector2) player.transform.position).normalized;
        rb.linearVelocity = knockbackDirection * knockbackForce;
    }
}
