using JetBrains.Annotations;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{ 
    public int                   MoneyToAdd = 10;
    public EnemyScriptableObject enemyData;
    public AudioClip             bruitagedegas;
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
        AudioSource.PlayClipAtPoint(bruitagedegas, transform.position);
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
        MonyManager.instance.AddScore(MoneyToAdd);
    }
}
