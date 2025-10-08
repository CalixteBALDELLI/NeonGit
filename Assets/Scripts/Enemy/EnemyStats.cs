using JetBrains.Annotations;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{ 
    public EnemyScriptableObject enemyData;
    
    //Current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        currentMoveSpeed  = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            kill();
        }
        
    }

    public void kill()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        if (es != null)
            es.OnEnemyKilled();
    }
}
