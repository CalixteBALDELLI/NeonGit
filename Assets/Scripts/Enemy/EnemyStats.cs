using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{

    public EnemyScriptableObject  enemyData;
    PlayerStats                   playerStats;
    public WeaponScriptableObject playerSword;
    public AudioClip              bruitagedegas;
    public int                    MoneyToAdd = 10;
    //Current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
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
            kill();
            AudioSource.PlayClipAtPoint(bruitagedegas, transform.position);
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

    

    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.gameObject.tag == "Player")
        {
            playerStats.currentHealth -= enemyData.Damage;
        }
        
        if (cl2D.gameObject.tag == "PlayerSword")
        {
            TakeDamage(playerSword.Damage);
        }
    }
}
