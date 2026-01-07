using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScieRuban : MonoBehaviour
{
    public float damagePerTick = 5f;
    public float tickRate = 0.5f;
    public float rotationSpeed = 100f; // Vitesse de rotation, ajustable depuis l'inspecteur

    private HashSet<EnemyStat> enemies = new HashSet<EnemyStat>();
    private Coroutine damageCoroutine;

    void Update()
    {
        // Faire tourner l'objet autour de lui-même
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime); // Rotation autour de l'axe Z (axe 2D)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null)
        {
            return;
        }

        // Vérifie que l'objet a le tag "Enemy"
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
        // Stoppe la coroutine si plus aucun ennemi
        if (enemies.Count == 0 && damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    IEnumerator DamageLoop()
    {
        // Si la vitesse du ruban n'est pas définie, la mettre à une valeur par défaut.
        WaitForSeconds wait = new WaitForSeconds(ModuleManager.SINGLETON.modulesData[19 + ModuleManager.SINGLETON.rubanAcquired].Speed);

        while (enemies.Count > 0)
        {
            List<EnemyStat> buffer = new List<EnemyStat>(enemies);

            foreach (EnemyStat enemy in buffer)
            {
                if (enemy != null)
                {
                    enemy.TakeDamage(ModuleManager.SINGLETON.modulesData[42 + ModuleManager.SINGLETON.rubanAcquired].Damage);
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
