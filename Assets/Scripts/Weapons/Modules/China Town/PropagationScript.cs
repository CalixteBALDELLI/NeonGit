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
    [SerializeField]         SpriteRenderer spriteRenderer;
    [SerializeField]         EnemyMouvement enemyMouvement;
    [SerializeField]         int            maxPropagationSteps;
    [SerializeField]         Color          baseColor;

    void Awake()
    {
        playerStats   = GameObject.Find("Player").GetComponent<PlayerStats>();
        moduleManager = GameObject.Find("GameManager").GetComponent<ModuleManager>();
        Debug.Log(moduleManager);
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


    public IEnumerator CallDamagingEnemyRepeatedly()
    {
        enemyMouvement.isElectrocuted       = true;
        moduleManager.propagationInProgress = true;
        for (int i = 0; i < howManyTimeDamagingEnemyIsCalled; i++)
        {
            DamageEnemy();
            yield return new WaitForSeconds(delayTimeBetweenDamage); // attend X secondes
        }

        Debug.Log("Boucle fini");
        enemyMouvement.isElectrocuted = false;
        Propagation();
    }

    
    void DamageEnemy()
    {
        StartCoroutine(ChangeEnemyColor());
        enemyStat.currentHealth -= playerStats.currentPlayerDamage / currentModuleDamages;
    }

    public void Propagation()
    {
        Debug.Log("Propagation");
        DistanceBetweenEnemies();
    }

    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.name);
            focusedEnemies.Add(other.GetComponent<EnemyStat>());
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log(other.name + "removed");
            focusedEnemies.Remove(other.GetComponent<EnemyStat>());
        }
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
