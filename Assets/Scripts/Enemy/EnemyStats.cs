using System.Collections;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    [HideInInspector] public PlayerStats playerStats;
    public WeaponScriptableObject[] knockbackData;
    
    [SerializeField] DropRateManager dropRateManager;
    [SerializeField] EnemyMouvement enemyMouvement;
    
    [SerializeField] CharacterScriptableObject playerScriptableObject;
    
    [SerializeField]        GameObject        propagationCollider;
    [SerializeField]        PropagationScript propagationScript;
    [SerializeField]        bool              isABoss;
    [SerializeField]        GameObject        teleporterKey;
    [SerializeField] public bool              isElectrocuted;
    [SerializeField] public bool              hitBySword;
    public                  bool              isDead;
    Canvas                                    KeyObtained;

    
    // Current stats
    float currentMoveSpeed;
    public float currentHealth;
    float currentDamage;
    
    [Header("Audio")]
    private AudioSource  Dama;
    private AudioSource  kill;
    void Awake()
    {
        // Initialisation des stats
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth    = enemyData.MaxHealth;
        currentDamage    = enemyData.Damage;

        //KeyObtained = GameObject.Find("KeyObtained").GetComponent<Canvas>();
    }

    IEnumerator Knockback()
    {
        if (ModuleManager.SINGLETON.knockbackAcquired > 0)
        {
            Debug.Log("Knockback");
            enemyMouvement.isKnockedBack = true;
            yield return new WaitForSeconds(0.5f);
            enemyMouvement.isKnockedBack = false;
        }
    }

    public void TakeDamage(float dmg)
    {
        propagationCollider.SetActive(true);
        propagationScript.hitBoxCollider2D.enabled = true;
        enemyMouvement.isStunned                   = true;

        currentHealth -= dmg;

        
        if (currentHealth > 0)
        {
            ModuleManager.GenerateFloatingText(dmg.ToString(), transform);
        }

        StartCoroutine(damageFlash());
    }

    
    void HealthCheck()
    {
        Debug.Log("Health Check");
        if (currentHealth <= 0)
        {
            isDead = true;
            if (isABoss)
            {
                PlayerStats.SINGLETON.teleporterKeyObtained = true;
            }
            
            if (isABoss == false && isElectrocuted && ModuleManager.SINGLETON.propagationInProgress)
            {
                if (ModuleManager.SINGLETON.currentPropagationStep < propagationScript.maxPropagationSteps)
                {
                    Debug.Log("Distance");
                    propagationScript.DistanceBetweenEnemies();
                    //ModuleManager.SINGLETON.currentPropagationStep--;
                }
                else if(ModuleManager.SINGLETON.currentPropagationStep == propagationScript.maxPropagationSteps)
                {
                    ModuleManager.SINGLETON.propagationInProgress = false;
                    ModuleManager.SINGLETON.currentPropagationStep = 0;
                }
            }
            Kill();
        }
        else
        {
            propagationCollider.SetActive(false);
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
        if (ModuleManager.SINGLETON.propagationAcquired > 0 && ModuleManager.SINGLETON.propagationInProgress == false)
        {
            ModuleManager.SINGLETON.propagationInProgress = true;
            Propage();
        }

        if (ModuleManager.SINGLETON.knockbackAcquired == 1)
        {
            enemyMouvement.currentKnockbackForce = knockbackData[0].Speed;
            StartCoroutine(Knockback());
        }
        else if (ModuleManager.SINGLETON.knockbackAcquired == 2)
        {
            Debug.Log("Knockback 2");
            enemyMouvement.currentKnockbackForce = knockbackData[1].Speed;
            StartCoroutine(Knockback());
        }
        else if (ModuleManager.SINGLETON.knockbackAcquired == 3)
        {
            Debug.Log("Knockback 3");
            enemyMouvement.currentKnockbackForce = knockbackData[2].Speed;
            StartCoroutine(Knockback());
        }
    }

    public void Propage()
    {
        propagationCollider.SetActive(true); // Active le collider et exécute le code pour la propagation.
        Debug.Log("Collider Activated");
        StartCoroutine(propagationScript.CallDamagingEnemyRepeatedly());
    }
    IEnumerator damageFlash()
    {
        enemyMouvement.isStunned                          = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        HealthCheck();
    }
}
