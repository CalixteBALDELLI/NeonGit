using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DégatFoudreScript : MonoBehaviour
{
    public PlayerStats playerStats;            // Référence aux stats du joueur
    public float foudreModuleDamages = 10;     // Diviseur des dégâts
    public float speedDamage = 0.25f;         // Intervalle entre les dégâts
    public float durationFoudre = 10f;  
    public EnemyStat enemyStat;

    private List<EnemyStat> enemiesInZone = new List<EnemyStat>();
    private bool isDamaging = false;

    void Start()
    {
        playerStats = PlayerStats.SINGLETON;
        StartCoroutine(DestroyAfterDuration());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStat enemy = other.GetComponent<EnemyStat>();
            if (enemy != null && !enemiesInZone.Contains(enemy))
            {
                enemiesInZone.Add(enemy);

                // Démarre la coroutine si ce n'est pas déjà fait
                if (!isDamaging)
                {
                    isDamaging = true;
                    StartCoroutine(DamageEnemies());
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStat enemy = other.GetComponent<EnemyStat>();
            if (enemy != null && enemiesInZone.Contains(enemy))
            {
                enemiesInZone.Remove(enemy);
            }
        }
    }

    IEnumerator DamageEnemies()
    {
        while (enemiesInZone.Count > 0)
        {
            foreach (EnemyStat enemy in enemiesInZone)
            {
                if (enemy == null) continue; // sécurité si l’ennemi meurt entre temps

                Debug.Log(enemy.name + " Before Damage: " + enemy.currentHealth);

                enemy.TakeDamage(playerStats.currentPlayerDamage / foudreModuleDamages);

                Debug.Log(enemy.name + " took damage. Health now: " + enemy.currentHealth);
            }

            yield return new WaitForSeconds(speedDamage);
        }

        isDamaging = false;
    }

    IEnumerator DestroyAfterDuration()
    {
        yield return new WaitForSeconds(durationFoudre);
        Destroy(gameObject);
    }
}