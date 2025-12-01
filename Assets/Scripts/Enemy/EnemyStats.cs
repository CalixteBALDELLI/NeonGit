using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    [SerializeField] DropRateManager dropRateManager;
    [SerializeField] EnemyMouvement enemyMouvement;
    [SerializeField] KnockBackModule knockBackModule;
    
    [SerializeField] CharacterScriptableObject playerScriptableObject;
    
    [SerializeField]        GameObject        propagationCollider;
    [SerializeField]        PropagationScript propagationScript;
    [SerializeField]        bool              isABoss;
    [SerializeField]        GameObject        teleporterKey;
    [SerializeField] public bool              isElectrocuted;
    [SerializeField] public bool              hitBySword;
    Canvas                                    KeyObtained;
    [SerializeField] public Collider2D        hitBoxCollider2D;
    public                  EnemyStat         attacker;
    public                  Vector3           spawnPosition;
    
    // Current stats
    float currentMoveSpeed;
    public float currentHealth;
    float currentDamage;
    
    [Header("Audio")]
    public  AudioClip Damage;
    public  AudioClip kill;
    void Awake()
    {
        // Initialisation des stats
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth    = enemyData.MaxHealth;
        currentDamage    = enemyData.Damage;

        spawnPosition = transform.position;
        //KeyObtained = GameObject.Find("KeyObtained").GetComponent<Canvas>();
    }

    

    public void TakeDamage(float dmg)
    {
        enemyMouvement.isStunned                   =  true;
        currentHealth                              -= dmg;
        StartCoroutine(DamageFlash());
        if (isElectrocuted == false)
        {
            HealthCheck();
        }
    }
    // ReSharper disable Unity.PerformanceAnalysis
    public IEnumerator DamageFlash()
    {
//        Debug.Log(transform.position + "DamageFlash");
        enemyMouvement.isStunned                          = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

    }

    public void HealthCheck()
    {
        //Debug.Log(transform.position + " Health Check");
        if (currentHealth <= 0)
        {
            if (isABoss)
            {
                PlayerStats.SINGLETON.teleporterKeyObtained = true;
            }
//            Debug.LogWarning(transform.position + " DIED");
            Kill();
        }
        else
        {
            enemyMouvement.isStunned = false;
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
        dropRateManager.BottleDrop();
    }

    private void OnDestroy()
    {
        EnemySpawner es = FindAnyObjectByType<EnemySpawner>();
        if (es != null)
            es.OnEnemyKilled();
    }

    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.CompareTag("Player"))
        {
           PlayerStats.SINGLETON.currentHealth -= enemyData.Damage;
        }

        if (cl2D.CompareTag("PlayerSword"))
        {
            if (ModuleManager.SINGLETON.propagationAcquired == 0)
            {
                //TakeDamage(playerScriptableObject.damages);
                ModulesCheck();
            }
            else
            {
                hitBySword = true;
                ModulesCheck();
            }
        }

        if (cl2D.CompareTag("Projectile"))
        {
            ModulesCheck();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
//        Debug.Log("Collision");
        if (enemyMouvement.isKnockedBack)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //Debug.Log(collision.gameObject.name);    
                KnockBackModule touchedEnemyMouvement = collision.gameObject.GetComponent<KnockBackModule>();
                touchedEnemyMouvement.currentKnockbackStep = knockBackModule.currentKnockbackStep + 1;
                Debug.Log(spawnPosition + " Current Knockback Step : " + touchedEnemyMouvement.currentKnockbackStep);
                Debug.Log(spawnPosition + " Damages = " + PlayerStats.SINGLETON.currentPlayerDamage + " / " + touchedEnemyMouvement.currentKnockbackStep + " = " + (PlayerStats.SINGLETON.currentPlayerDamage / touchedEnemyMouvement.currentKnockbackStep));
                touchedEnemyMouvement.enemyStat.TakeDamage(PlayerStats.SINGLETON.currentPlayerDamage / touchedEnemyMouvement.currentKnockbackStep);
                touchedEnemyMouvement.KnockbackSetup();
            }
        }
    }
    
    void ModulesCheck()
    {
        if (ModuleManager.SINGLETON.propagationAcquired > 0)
        {
            if (ModuleManager.SINGLETON.propagationCooldown <= 0)
            {
                Propage();
                //StartCoroutine(ModuleManager.SINGLETON.PropagationCooldown());
            }
        }

        if (ModuleManager.SINGLETON.knockbackAcquired > 0)
        {
            knockBackModule.KnockbackSetup();
        }
    }

    public void Propage()
    {
//        Debug.Log(transform.position + "Propaged");
        propagationCollider.SetActive(true);
        //StartCoroutine(propagationScript.CheckColliderActivation());
        // Active le collider et exécute le code pour la propagation.
//      Debug.Log("Collider Activated");
         //propagationScript.DistanceBetweenEnemies();
    }
}
