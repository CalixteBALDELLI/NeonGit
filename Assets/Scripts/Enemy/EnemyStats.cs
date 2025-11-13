using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    PlayerStats playerStats;
    public WeaponScriptableObject playerSword;
    [SerializeField] DropRateManager dropRateManager;
    [SerializeField] EnemyMouvement enemyMouvement;
    
    [HideInInspector] public ModuleManager moduleManager;
    [SerializeField]         GameObject    propagationCollider;
    [SerializeField]         bool          isABoss;
    [SerializeField]         GameObject    teleporterKey;

    // Current stats
    float currentMoveSpeed;
    [SerializeField] float currentHealth;
    float currentDamage;

    void Awake()
    {
        // Initialisation des stats
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth    = enemyData.MaxHealth;
        currentDamage    = enemyData.Damage;

        playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        moduleManager = GameObject.Find("GameManager").GetComponent<ModuleManager>();
    }

    IEnumerator Knockback()
    {
        if (moduleManager.knockbackAcquired)
        {
            Debug.Log("Knockback");
            enemyMouvement.isKnockedBack = true;
            yield return new WaitForSeconds(0.5f);
            enemyMouvement.isKnockedBack = false;
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            if (isABoss)
            {
                Instantiate(teleporterKey, transform.position, Quaternion.identity);
            }
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
            if (moduleManager.knockbackAcquired)
            {
            StartCoroutine(Knockback());
            }
            
            if (moduleManager.propagationAcquired)
            {
            propagationCollider.SetActive(true); // Active le collider et exécute le code pour la propagation.
            }
            
            TakeDamage(playerStats.currentSwordDamages);
        }
    }
}
