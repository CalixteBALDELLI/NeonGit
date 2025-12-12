using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegatFoudreScript : MonoBehaviour
{
    public PlayerStats              playerStats;
    public float                    foudreModuleDamages = 10f;
    public float                    speedDamage         = 0.25f;
    public float                    durationFoudre      = 10f;
    public WeaponScriptableObject[] foudreData;


    
    private readonly HashSet<EnemyStat> enemiesInZone = new HashSet<EnemyStat>();

    private bool isDamaging = false;

    
    private WaitForSeconds damageDelay;
    private WaitForSeconds destroyDelay;

    private float damagePerTick;

    void Awake()
    {
        foudreModuleDamages = foudreData[ModuleManager.SINGLETON.foudreAcquired - 1].Damage;
        speedDamage         = foudreData[ModuleManager.SINGLETON.foudreAcquired - 1].Speed;
        durationFoudre      = foudreData[ModuleManager.SINGLETON.foudreAcquired - 1].duration;
        Debug.Log("Foudre : " + durationFoudre);
        damageDelay         = new WaitForSeconds(speedDamage);
        destroyDelay        = new WaitForSeconds(durationFoudre);
    }

    void Start()
    {
        playerStats = PlayerStats.SINGLETON;
        damagePerTick = playerStats.currentPlayerDamage / foudreModuleDamages;

        StartCoroutine(DestroyAfterDuration());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyStat e = other.GetComponent<EnemyStat>();
        if (e == null) return;

        
        if (enemiesInZone.Add(e) && !isDamaging)
        {
            isDamaging = true;
            StartCoroutine(DamageLoop());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyStat e = other.GetComponent<EnemyStat>();
        if (e == null) return;

        enemiesInZone.Remove(e);
    }

    IEnumerator DamageLoop()
    {
        // Buffer réutilisé pour éviter toute allocation pendant le jeu
        List<EnemyStat> buffer = new List<EnemyStat>(32);

        while (enemiesInZone.Count > 0)
        {
            buffer.Clear();

            // On copie le contenu du HashSet dans un buffer temporaire
            foreach (EnemyStat e in enemiesInZone)
                buffer.Add(e);

            // Maintenant on peut boucler SANS risque
            for (int i = 0; i < buffer.Count; i++)
            {
                EnemyStat e = buffer[i];
                if (e == null) continue;

                e.TakeDamage(damagePerTick);
            }

            // Nettoyage des nulls APRÈS l’itération
            enemiesInZone.RemoveWhere(item => item == null);

            yield return damageDelay;
        }

        isDamaging = false;
    }

    IEnumerator DestroyAfterDuration()
    {
        yield return destroyDelay;
        Destroy(gameObject);
    }
//Code Avant L'Opti    
// using System.Collections;

// using System.Collections.Generic;
// using UnityEngine;

// public class DégatFoudreScript : MonoBehaviour
// {
//     public PlayerStats playerStats;            // Référence aux stats du joueur
//     public float foudreModuleDamages = 10;     // Diviseur des dégâts
//     public float speedDamage = 0.25f;         // Intervalle entre les dégâts
//     public float durationFoudre = 10f;  
//     public EnemyStat enemyStat;

//     private List<EnemyStat> enemiesInZone = new List<EnemyStat>();
//     private bool isDamaging = false;

//     void Start()
//     {
//         playerStats = PlayerStats.SINGLETON;
//         StartCoroutine(DestroyAfterDuration());
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Enemy"))
//         {
//             EnemyStat enemy = other.GetComponent<EnemyStat>();
//             if (enemy != null && !enemiesInZone.Contains(enemy))
//             {
//                 enemiesInZone.Add(enemy);

//                 // Démarre la coroutine si ce n'est pas déjà fait
//                 if (!isDamaging)
//                 {
//                     isDamaging = true;
//                     StartCoroutine(DamageEnemiesFoudre());
//                 }
//             }
//         }
//     }

//     void OnTriggerExit2D(Collider2D other)
//     {
//         if (other.CompareTag("Enemy"))
//         {
//             EnemyStat enemy = other.GetComponent<EnemyStat>();
//             if (enemy != null && enemiesInZone.Contains(enemy))
//             {
//                 enemiesInZone.Remove(enemy);
//             }
//         }
//     }

//     IEnumerator DamageEnemiesFoudre()
//     {
//         while (enemiesInZone.Count > 0)
//         {
//             foreach (EnemyStat enemy in enemiesInZone)
//             {
//                 if (enemy == null) continue; // sécurité si l’ennemi meurt entre temps

//                 Debug.Log(enemy.name + " Before Damage: " + enemy.currentHealth);

//                 enemy.TakeDamage(playerStats.currentPlayerDamage / foudreModuleDamages);

//                 Debug.Log(enemy.name + " took damage. Health now: " + enemy.currentHealth);
//             }

//             yield return new WaitForSeconds(speedDamage);
//         }

//         isDamaging = false;
//     }

//     IEnumerator DestroyAfterDuration()
//     {
//         yield return new WaitForSeconds(durationFoudre);
//         Destroy(gameObject);
//     }
// }
    
}