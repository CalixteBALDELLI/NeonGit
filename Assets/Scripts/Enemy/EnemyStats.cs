using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [HideInInspector] public PlayerStats playerStats;
    public WeaponScriptableObject[] knockbackData;
    
    [SerializeField] DropRateManager dropRateManager;
    [SerializeField] EnemyMouvement enemyMouvement;
    
    [SerializeField] CharacterScriptableObject playerScriptableObject;
    
    [HideInInspector] public ModuleManager     moduleManager;
    [SerializeField]         GameObject        propagationCollider;
    [SerializeField]         PropagationScript propagationScript;
    [SerializeField]         bool              isABoss;
    [SerializeField]         GameObject        teleporterKey;

    // Current stats
    float currentMoveSpeed;
    public float currentHealth;
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
        if (moduleManager.knockbackAcquired > 0)
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
            ModulesCheck();
            TakeDamage(playerScriptableObject.damages);
        }

        if (cl2D.CompareTag("Projectile"))
        {
            ModulesCheck();
        }

        if (cl2D.CompareTag("Enemy"))
        {
            //EnemyStat touchedEnemy = cl2D.GetComponent<EnemyStat>;
            
        }
    }

    void ModulesCheck()
    {
        if (moduleManager.propagationAcquired == 1 && moduleManager.propagationInProgress == false)
        {
            Propage();
            StartCoroutine(moduleManager.BackupTimer());
            moduleManager.BACKUPTIMER = 0;
        }

        if (moduleManager.knockbackAcquired == 1)
        {
            enemyMouvement.currentKnockbackForce = knockbackData[0].Speed;
            StartCoroutine(Knockback());
        }
        else if (moduleManager.knockbackAcquired == 2)
        {
            Debug.Log("Knockback 2");
            enemyMouvement.currentKnockbackForce = knockbackData[1].Speed;
            StartCoroutine(Knockback());
        }
        else if (moduleManager.knockbackAcquired == 3)
        {
            Debug.Log("Knockback 3");
            enemyMouvement.currentKnockbackForce = knockbackData[2].Speed;
            StartCoroutine(Knockback());
        }
    }

    public void Propage()
    {
        propagationCollider.SetActive(true); // Active le collider et exécute le code pour la propagation.
        StartCoroutine(propagationScript.CallDamagingEnemyRepeatedly());
    }
}
