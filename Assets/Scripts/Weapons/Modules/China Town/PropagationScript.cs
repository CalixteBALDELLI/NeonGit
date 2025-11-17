using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PropagationScript : MonoBehaviour
{
    // Variables module Propagation
    [SerializeField] List<float>            distances      = new List<float>();
    [SerializeField] List<EnemyStat>        focusedEnemies = new List<EnemyStat>();
    int                                     shortestDistanceIndex;
    [SerializeField] Collider2D             hitBoxCollider2D;
    int                                     howManyTimeDamagingEnemyIsCalled = 10;
    float                                   delayTimeBetweenDamage           = 0.5f; //en seconde
    [SerializeField]         float          currentModuleDamages;
    [SerializeField]         EnemyStat      enemyStat;
    [HideInInspector] public PlayerStats    playerStats;
    [HideInInspector] public ModuleManager  moduleManager;
    [SerializeField]         Color          baseColor;
    [SerializeField]         SpriteRenderer spriteRenderer;
    [SerializeField]         int            maxPropagationSteps;
    [SerializeField]         EnemyMouvement enemyMouvement;

    void Start()
    {
        playerStats   = GameObject.Find("Player").GetComponent<PlayerStats>();
        moduleManager = GameObject.Find("GameManager").GetComponent<ModuleManager>();
        if (moduleManager.projectileAcquired == 1)
        {
            maxPropagationSteps = 2;
        }
        
        if (moduleManager.propagationAcquired == 2)
        {
            maxPropagationSteps = 4;
        }

        if (moduleManager.propagationAcquired == 3)
        {
            maxPropagationSteps = 6;
        }
        hitBoxCollider2D.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
            focusedEnemies.Add(other.GetComponent<EnemyStat>());
        }
    }

    public IEnumerator CallDamagingEnemyRepeatedly()
    {
        for (int i = 0; i < howManyTimeDamagingEnemyIsCalled; i++)
        {
            enemyMouvement.isElectrocuted       = true;
            moduleManager.propagationInProgress = true;
            DamagingEnemy();

            if (enemyStat.currentHealth <= 1)
            {
                Propagation();
                moduleManager.propagationInProgress = true;
                yield break;
            }

            yield return new WaitForSeconds(delayTimeBetweenDamage); // attend X secondes
        }

        Debug.Log("Boucle fini");
        enemyMouvement.isElectrocuted = false;
        Propagation();
    }


    void DamagingEnemy()
    {
        StartCoroutine(ChangeEnemyColor());
        enemyStat.currentHealth -= playerStats.currentPlayerDamage / currentModuleDamages;
    }

    public void Propagation()
    {
        Debug.Log("Propagation");
        DistanceBetweenEnemies();
            
            //Debug.Log("propagationBool = false");
            //Debug.Log("prend l'enemies frappé par l'epper et le met dans la variable enemyTargeted");
            //Debug.Log("créer un while qui dur aussi longtemp que les stat du scriptable object du module propagation");
            //Debug.Log("Appelle la fonction propagation Effect");
            //Debug.Log("créé hit box autour de enemy targeted");
            //Debug.Log("récupere tout les ennemies dans la hitbox");
            //Debug.Log("prend le premiere ennemie stock le dans la variable closestEnemy ");
            //Debug.Log("prend le deuxiemme enemy et compare si il est plus proche de enemy targeted que cosestEnemy n'est");
            //Debug.Log("remplace l'enemy dans la variable si oui");
            //Debug.Log("continue a faire ça pour chaque enemies dans la hit box");
            //Debug.Log("puis met closestEnemy dans enemyTargeted");
            //Debug.Log("une fois la boucle finis propagationbool = true");
    }


    public void DistanceBetweenEnemies() // Mesure la distance de chacun d'eux par rapport à l'ennemi initiateur de la propagation (en excluant ce dernier) et l'ajoute dans une liste.
    {
        Debug.Log("Distance between enemies");
        focusedEnemies.Remove(enemyStat);
        foreach (EnemyStat inFocus in focusedEnemies)
        {
            distances.Add(Vector3.Distance(inFocus.transform.position, gameObject.transform.position));
        }
        LookForSmallestDistance();
    }

    void LookForSmallestDistance() // Sélectionne la distance la plus petite dans la liste, correspondant à l'ennemi le plus proche de celui initiateur de la propagation.
    {
        Debug.Log("Looking for smallest distance");
        float minVal = distances.Min();
        shortestDistanceIndex = distances.IndexOf(minVal);
        TransmitToNextEnemy();
    }

    void TransmitToNextEnemy()
    {
        if (moduleManager.currentPropagationStep < maxPropagationSteps)
        {
            focusedEnemies[shortestDistanceIndex].Propage();
            moduleManager.currentPropagationStep++;
            gameObject.SetActive(false);
        }
        else if (moduleManager.currentPropagationStep == maxPropagationSteps)
        {
            moduleManager.propagationInProgress = false;
            moduleManager.currentPropagationStep = 0;
        }
    }

    IEnumerator ChangeEnemyColor() // Changement de couleur représentant la prise de dégâts par l'ennemi
    {
        spriteRenderer.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.GetComponent<SpriteRenderer>().color = baseColor;
    }
}
