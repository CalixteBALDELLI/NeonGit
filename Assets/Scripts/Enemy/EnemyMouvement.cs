using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    public  EnemyScriptableObject enemyData;
    private Transform             player;
    public  bool                  isKnockedBack;
    public  EnemyStat             enemyStat;
    public  float                 currentKnockbackForce;
    public  Rigidbody2D           rb;
    public  bool                  isElectrocuted;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isKnockedBack == false && isElectrocuted == false)
        { 
            rb.linearVelocity = Vector2.zero;
            transform.position               = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime);
        }
        else if (isKnockedBack && isElectrocuted == false)
        {
            KnockbackMovement();   
        }
    }

    void KnockbackMovement()
    {
        Vector2 knockbackDirection = (rb.position - (Vector2) player.transform.position).normalized;
        rb.linearVelocity = knockbackDirection * currentKnockbackForce;
    }
}
