using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PropagationScript : MonoBehaviour
{
    
    
    // Variables module Propagation
    [SerializeField] List<float>            distances      = new List<float>();
    [SerializeField] List<EnemyStat>        focusedEnemies = new List<EnemyStat>();
    int                                     shortestDistanceIndex;
    [SerializeField] public Collider2D      hitBoxCollider2D;
    int                                     howManyTimeDamagingEnemyIsCalled = 10;
    float                                   delayTimeBetweenDamage           = 0.5f; //en seconde
    [SerializeField]         float          currentModuleDamages;
    [SerializeField]         EnemyStat      enemyStat;
    [HideInInspector] public PlayerStats    playerStats;
    [HideInInspector] public ModuleManager  moduleManager;
    [SerializeField]         SpriteRenderer spriteRenderer;
    [SerializeField]         EnemyMouvement enemyMouvement;
    [SerializeField] public  int            maxPropagationSteps;
    [SerializeField]         Color          baseColor;
    [SerializeField] public  bool           enemyDetection;

    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {
        if (enemyDetection)
        {
            Debug.LogWarning("Enemy Dtteted");
            focusedEnemies.Add(other.GetComponent<EnemyStat>());
        }
    }
    
    void Awake()
    {
        playerStats   = PlayerStats.SINGLETON;
        moduleManager = ModuleManager.SINGLETON;
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
    }


    public IEnumerator CallDamagingEnemyRepeatedly()
    {
        enemyMouvement.isElectrocuted       = true;
        for (int i = 0; i < howManyTimeDamagingEnemyIsCalled; i++)
        {
            DamageEnemy();
            yield return new WaitForSeconds(delayTimeBetweenDamage); // attend X secondes
            if (i < howManyTimeDamagingEnemyIsCalled - 1)
            {
                enemyDetection = true;
            }
        }
        enemyMouvement.isElectrocuted = false;
        DistanceBetweenEnemies();
    }

    
    void DamageEnemy()
    {
        StartCoroutine(ChangeEnemyColor());
        enemyStat.currentHealth -= playerStats.currentPlayerDamage / currentModuleDamages;
    }
    
	public void DistanceBetweenEnemies() // Mesure la distance de chacun d'eux par rapport à l'ennemi initiateur de la propagation (en excluant ce dernier) et l'ajoute dans une liste.
    {
        //Debug.Log("Distance between enemies");
        focusedEnemies.Remove(enemyStat);
        foreach (EnemyStat inFocus in focusedEnemies)
        {
            if (inFocus != null)
            {
                Debug.Log("Enemy Added");
                distances.Add(Vector3.Distance(inFocus.transform.position, gameObject.transform.position));
            }
        }

        if (distances.Count > 0)
        {
            LookForSmallestDistance();
        }
        else
        {
            Debug.Log("propagaton eneded");
            EndPropagation();
        }
    }

    void LookForSmallestDistance() // Sélectionne la distance la plus petite dans la liste, correspondant à l'ennemi le plus proche de celui initiateur de la propagation.
    {
        //Debug.Log("Looking for smallest distance");
        float minVal = distances.Min();
        
		//Debug.Log(minVal);
        shortestDistanceIndex = distances.IndexOf(minVal);
        TransmitToNextEnemy();
        
    }

    void TransmitToNextEnemy()
    {
        if (moduleManager.currentPropagationStep < maxPropagationSteps)
        {
            if (focusedEnemies[shortestDistanceIndex].CompareTag("Enemy") && focusedEnemies[shortestDistanceIndex] != null)
            {
                //Debug.Log(moduleManager.currentPropagationStep);
                focusedEnemies[shortestDistanceIndex].Propage();
                moduleManager.currentPropagationStep++;
                distances.Clear();
                enemyDetection = false;
                //gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Enemy not found");
                focusedEnemies.RemoveAt(shortestDistanceIndex);
                LookForSmallestDistance();
            }
        }
        else if (moduleManager.currentPropagationStep == maxPropagationSteps)
        {
            EndPropagation();
        }
    }

    void EndPropagation()
    {
        moduleManager.propagationInProgress  = false;
        moduleManager.currentPropagationStep = 0;
        distances.Clear();
        //gameObject.SetActive(false);
    }

    IEnumerator ChangeEnemyColor() // Changement de couleur représentant la prise de dégâts par l'ennemi
    {
        spriteRenderer.GetComponent<SpriteRenderer>().color = Color.yellow;
        yield return new WaitForSeconds(0.25f);
        spriteRenderer.GetComponent<SpriteRenderer>().color = baseColor;
    }
}
