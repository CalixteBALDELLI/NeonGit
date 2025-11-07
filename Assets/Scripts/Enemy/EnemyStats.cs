using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    PlayerStats playerStats;
    public WeaponScriptableObject playerSword;
    [SerializeField] DropRateManager dropRateManager;

    [Header("Référence vers le prefab du ModuleManager")]
    public ModuleManager moduleManagerPrefab;
    private static ModuleManager moduleManagerInstance;

    
    public Rigidbody2D rb;
    public EnemyMouvement ennemyMovement;
    
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

        // 👇 Vérifie si le ModuleManager existe déjà dans la scène
        if (moduleManagerInstance == null)
        {
            moduleManagerInstance = Instantiate(moduleManagerPrefab);
            moduleManagerInstance.name = "ModuleManager (Instance)";
            DontDestroyOnLoad(moduleManagerInstance.gameObject); // garde-le si tu changes de scène
        }
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

    IEnumerator Knockback()
    {
        ennemyMovement.isAttacked = true;
        yield return new WaitForSeconds(0.5f);
        ennemyMovement.isAttacked = false;
        rb.linearVelocity = Vector2.zero;
        Debug.Log("Player hit by enemy");
    }
    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.CompareTag("Player"))
        {
            playerStats.currentHealth -= enemyData.Damage;
        }

        if (cl2D.CompareTag("PlayerSword"))
        {
            TakeDamage(playerSword.Damage);
            StartCoroutine(Knockback());
            //moduleManagerInstance.Propagation();
        }
    }
}
