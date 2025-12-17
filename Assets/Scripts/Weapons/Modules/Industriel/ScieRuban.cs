using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScieRuban : MonoBehaviour
{
    public float damagePerTick = 5f;
    public float tickRate = 0.5f;

    private HashSet<EnemyStat> enemies = new HashSet<EnemyStat>();
    private Coroutine damageCoroutine;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }

        // Vérifie que l'objet a le tag Enemy
        if (!other.CompareTag("Enemy"))
        {
            return;
        }
        
        EnemyStat enemy = other.GetComponent<EnemyStat>();
        if (enemy == null)
        {
            return;
        }

        // Ajoute l'ennemi à la liste
        if (!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }

        // Démarre la coroutine de dégâts si ce n'est pas déjà fait
        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(DamageLoop());
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Stop la coroutine si plus aucun ennemi
        if (enemies.Count == 0 && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    IEnumerator DamageLoop()
    {
        if (tickRate <= 0f) tickRate = 0.1f;

        WaitForSeconds wait = new WaitForSeconds(tickRate);

        while (enemies.Count > 0)
        {
            List<EnemyStat> buffer = new List<EnemyStat>(enemies);

            foreach (EnemyStat enemy in buffer)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(damagePerTick);
                }
                else
                {
                    enemies.Remove(enemy);
                    
                }
            }

            yield return wait;
        }

        damageCoroutine = null;
    }
}
