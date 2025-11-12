using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    PlayerStats playerStats;
    public WeaponScriptableObject playerSword;
    [SerializeField] DropRateManager dropRateManager;
    
    public ModuleManager   moduleManager;
    public KnockBackModule knockBackModule;
    [SerializeField] GameObject        propagationCollider;

    // Current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        // Initialisation des stats
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth    = enemyData.MaxHealth;
        currentDamage    = enemyData.Damage;

        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        
        
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        dropRateManager.BottleDrop();
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        if (es != null)
            es.OnEnemyKilled();
    }

    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.CompareTag("Player"))
        {
            playerStats.currentHealth -= enemyData.Damage;
        }

        if (cl2D.CompareTag("PlayerSword"))
        {
            StartCoroutine(knockBackModule.Knockback());
            propagationCollider.SetActive(true); // Active le collider et exécute le code pour la propagation.
            TakeDamage(playerStats.currentSwordDamages);
            //moduleManager.Propagation();
            
        }
    }
}
