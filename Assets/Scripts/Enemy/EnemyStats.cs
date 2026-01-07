using System.Collections;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    [SerializeField] DropRateManager dropRateManager;
    [SerializeField] EnemyMouvement enemyMouvement;
    [SerializeField] KnockBackModule knockBackModule;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] CharacterScriptableObject playerScriptableObject;
    
    [SerializeField] GameObject propagationCollider;
    [SerializeField] PropagationScript propagationScript;
    [SerializeField] bool isABoss;
    [SerializeField] public bool isFinalBoss;
    [SerializeField] GameObject teleporterKey;
    [SerializeField] public bool isElectrocuted;
    [SerializeField] public bool hitBySword;
    Canvas KeyObtained;
    [SerializeField] public Collider2D hitBoxCollider2D;
    [HideInInspector] public EnemyStat attacker;
    public Vector3 spawnPosition;
    public bool isBleeding;
    [SerializeField] Canvas victoryScreen;

    // Current stats
    float currentMoveSpeed;
    public float currentHealth;
    float currentDamage;
    

    // Liste de sons aléatoires
    [Header("Sounds")]
    public AudioClip[] damageSounds; // Liste des sons de dégâts
    public AudioClip[] deathSounds;  // Liste des sons de mort

    public AudioSource audioSource; // Référence à l'AudioSource

    void Awake()
    {
        // Initialisation des stats
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;

        spawnPosition = transform.position;

        // Récupérer l'AudioSource attaché à cet objet
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log("tOOK daMAGE");
        enemyMouvement.isStunned = true;
        currentHealth -= dmg;

        // Joue un son de dégâts aléatoire si la liste n'est pas vide
        if (damageSounds.Length > 0)
        {
            AudioClip randomDamageSound = damageSounds[Random.Range(0, damageSounds.Length)];
            audioSource.PlayOneShot(randomDamageSound);
        }

        if (ModuleManager.SINGLETON.saignementCooldownFinished && ModuleManager.SINGLETON.saignementAcquired > 0)
        {
            GetComponent<Saignement>()?.CallSaignememnt();
        }
        
        StartCoroutine(DamageFlash());

        if (isElectrocuted == false)
        {
            HealthCheck();
        }
    }

    public IEnumerator DamageFlash()
    {
        enemyMouvement.isStunned = true;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.2f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(.2f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(.2f);
        spriteRenderer.enabled = true;
    }

    public void HealthCheck()
    {
        if (currentHealth <= 0)
        {
            if (isABoss)
            {
                PlayerStats.SINGLETON.teleporterKeyObtained = true;
                Timer.SINGLETON.objectiveMessage.text = "Clé du téléporteur obtenue. Fuyez vers celui-ci !";
                Canvas teleporterArrow = GameObject.FindWithTag("TeleporterArrow").GetComponent<Canvas>();
                teleporterArrow.enabled = true;
            }
            if (isFinalBoss)
            {
                victoryScreen = GameObject.Find("Victory Screen").GetComponent<Canvas>();
                victoryScreen.enabled = true;
                Time.timeScale = 0;
            }

            Kill();
        }
        else
        {
            enemyMouvement.isStunned = false;
        }
    }

    public void Kill()
    {
        // Joue un son de mort aléatoire
        if (deathSounds.Length > 0)
        {
            AudioClip randomDeathSound = deathSounds[Random.Range(0, deathSounds.Length)];
            // Crée un objet temporaire pour jouer le son de la mort
            GameObject audioObject = new GameObject("AudioObject");
            AudioSource tempAudioSource = audioObject.AddComponent<AudioSource>();
            tempAudioSource.PlayOneShot(randomDeathSound);
            Destroy(audioObject, randomDeathSound.length); // Détruit l'objet audio après la fin du son
        }

        
        dropRateManager.BottleDrop();
        Destroy(gameObject);
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
                TakeDamage(playerScriptableObject.damages);
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
            TakeDamage(ModuleManager.SINGLETON.modulesData[ModuleManager.SINGLETON.projectileAcquired].Damage * PlayerStats.SINGLETON.currentPlayerDamage);
            ModulesCheck();
        }

        if (cl2D.CompareTag("ScieRebondissante"))
        {
            TakeDamage(ModuleManager.SINGLETON.modulesData[23 + ModuleManager.SINGLETON.rebondAcquired].Damage * PlayerStats.SINGLETON.currentPlayerDamage);
            ModulesCheck();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemyMouvement.isKnockedBack)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                KnockBackModule touchedEnemyMouvement = collision.gameObject.GetComponent<KnockBackModule>();
                touchedEnemyMouvement.currentKnockbackStep = knockBackModule.currentKnockbackStep + 1;
                touchedEnemyMouvement.enemyStat.TakeDamage(PlayerStats.SINGLETON.currentPlayerDamage / touchedEnemyMouvement.currentKnockbackStep);
                touchedEnemyMouvement.KnockbackSetup();
            }
        }
    }

    public void ModulesCheck()
    {
        if (ModuleManager.SINGLETON.propagationAcquired > 0 && ModuleManager.SINGLETON.propagationCooldownFinished)
        {
            ModuleManager.SINGLETON.StartPropagationCooldown();
            Propage();
        }

        if (ModuleManager.SINGLETON.knockbackAcquired > 0 && ModuleManager.SINGLETON.knockbackCooldownFinished)
        {
            knockBackModule.KnockbackSetup();
            ModuleManager.SINGLETON.StartKnockbackCooldown();
        }
    }

    public void Propage()
    {
        StartCoroutine(propagationScript.CallDamagingEnemyRepeatedly());
    }
}
