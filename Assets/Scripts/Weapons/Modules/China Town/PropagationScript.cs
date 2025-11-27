using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
    Vector3                                 spawnPosition;
    bool                                    enemiesAdded;
    [SerializeField] Light2D                electrocutionLight;
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
        
        spawnPosition            = transform.position;
        
        if (enemyStat.hitBySword)
        {
            //Debug.LogWarning(spawnPosition + " Hit by sword");
        }
        enemyStat.isElectrocuted = true;
        hitBoxCollider2D.enabled = true;
        electrocutionLight.enabled = true;
        StartCoroutine(CheckColliderActivation());

    }

    public IEnumerator CheckColliderActivation()
    {
        do
        {
            yield return null;
            //Debug.Log(spawnPosition + " Collider Not Effective");

        } while (enemiesAdded == false);
        
        DistanceBetweenEnemies();
        
    }
    

    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {
        //Debug.Log(spawnPosition + " Has Detected : " + other.name);
        focusedEnemies.Add(other.GetComponent<EnemyStat>());
        enemiesAdded = true;
    }
    
    // Mesure la distance de chacun d'eux par rapport à l'ennemi initiateur de la propagation (en excluant ce dernier) et l'ajoute dans une liste.
	public void DistanceBetweenEnemies()
    {
        //Debug.Log(spawnPosition + "Added Distances between enemies");
        hitBoxCollider2D.enabled = false;
        distances.Clear();
        focusedEnemies.Remove(enemyStat);
        focusedEnemies.Remove(enemyStat.attacker);
        foreach (EnemyStat inFocus in focusedEnemies)
        {
            if (inFocus != null)
            {
//                Debug.Log(spawnPosition + " Added : " +  inFocus.name + " to the List.");
                distances.Add(Vector3.Distance(inFocus.transform.position, gameObject.transform.position));
            }
        }
        LookForSmallestDistance();

        
    }

    void LookForSmallestDistance() // Sélectionne la distance la plus petite dans la liste, correspondant à l'ennemi le plus proche de celui initiateur de la propagation.
    {
        if (distances.Count > 0)
        {
            //Debug.Log(spawnPosition + "Looked for smallest distance");
            float minVal = distances.Min();
            shortestDistanceIndex = distances.IndexOf(minVal);
            //Debug.Log(spawnPosition + " INDEX : "        + shortestDistanceIndex);
            //Debug.Log(spawnPosition + " Electrocuted : " + enemyStat.isElectrocuted);
            TransmitToNextEnemy();
        }
        else
        { 
            //Debug.Log(spawnPosition + " No enemy detected : Propagation Ended");
            EndPropagation();
        }
    }

    void TransmitToNextEnemy()
    {
        if (moduleManager.currentPropagationStep < maxPropagationSteps)
        {
            if (focusedEnemies[shortestDistanceIndex] != null && focusedEnemies[shortestDistanceIndex].CompareTag("Enemy") && focusedEnemies[shortestDistanceIndex].isElectrocuted == false)
            {
                //Debug.LogWarning(spawnPosition + " Transmitted");
                ModuleManager.SINGLETON.currentPropagationStep++;
                focusedEnemies[shortestDistanceIndex].Propage();
                focusedEnemies[shortestDistanceIndex].attacker = enemyStat;
            }
            else
            {
                //Debug.Log(spawnPosition + " Target Enemy was Dead");
                focusedEnemies.RemoveAt(shortestDistanceIndex);
                distances.RemoveAt(shortestDistanceIndex);
                LookForSmallestDistance();
            }
        }
        else
        {
            EndPropagation();
        }
        StartCoroutine(CallDamagingEnemyRepeatedly());
    }


    public void EndPropagation()
    {
        moduleManager.propagationInProgress  = false;
        moduleManager.currentPropagationStep = 0;
    }

    public IEnumerator CallDamagingEnemyRepeatedly()
    {
        spriteRenderer.GetComponent<SpriteRenderer>().color = Color.yellow; // A CHANGER EN UN GLISSER DEPOSER
        for (int i = 0; i < howManyTimeDamagingEnemyIsCalled; i++)
        {
            enemyStat.TakeDamage(0);   
            yield return new WaitForSeconds(delayTimeBetweenDamage); // attend X secondes
        }
        // Boucle terminée
        //Debug.Log(spawnPosition + " Boucle finie");
        spriteRenderer.GetComponent<SpriteRenderer>().color = baseColor;
        if (moduleManager.currentPropagationStep == maxPropagationSteps)
        {
            EndPropagation();
        }
        DisableCollider();
    }
    void DisableCollider()
    {
        electrocutionLight.enabled = false;
        enemyStat.isElectrocuted   = false;
        distances.Clear();
        if (enemyStat.hitBySword)
        {
            enemyStat.TakeDamage(playerStats.currentPlayerDamage);
        }
        enemyStat.HealthCheck();
        enemyMouvement.isStunned = false;
        hitBoxCollider2D.enabled = false;
        gameObject.SetActive(false);
    }
}
