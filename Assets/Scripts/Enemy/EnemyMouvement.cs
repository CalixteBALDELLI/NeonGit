using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    public  EnemyScriptableObject enemyData;
    private Transform             player;
    public  bool                  isKnockedBack;
    public  EnemyStat             enemyStat;
    public  float                 currentKnockbackForce;
    public  Rigidbody2D           rb;
    public  bool                  isStunned;

    [Header("Obstacle Avoidance")]
    public float obstacleCheckDistance = 1f;
    public float     avoidStrength = 3f;
    public LayerMask obstacleLayer;

    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (!isKnockedBack && !isStunned)
        {
            MoveTowardsPlayer();
        }
        else if (isKnockedBack && enemyStat.isElectrocuted == false)
        {
            KnockbackMovement();
        }
    }

    void MoveTowardsPlayer()
    {
        rb.linearVelocity = Vector2.zero;

        // direction brute vers le joueur
        Vector2 dir = (player.position - transform.position).normalized;

        // raycast pour détecter un obstacle devant l'ennemi
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, obstacleCheckDistance, obstacleLayer);

        if (hit.collider != null)
        {
            // calcul d'une direction de contournement
            Vector2 avoidDir = Vector2.Perpendicular(hit.normal) * avoidStrength;

            dir = (dir + avoidDir).normalized;
        }

        transform.position += (Vector3)(dir * enemyData.MoveSpeed * Time.deltaTime);
    }

    void KnockbackMovement()
    {
        Vector2 knockbackDirection = (rb.position - (Vector2)player.transform.position).normalized;
        rb.linearVelocity = knockbackDirection * currentKnockbackForce;
    }
}

