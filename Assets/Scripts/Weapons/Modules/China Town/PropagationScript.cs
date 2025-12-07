using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PropagationScript : MonoBehaviour
{
    
    
    // Variables module Propagation
    [SerializeField]        List<float>     distances      = new List<float>();
    [SerializeField] public List<EnemyStat> focusedEnemies = new List<EnemyStat>();
    int                                     shortestDistanceIndex;
    [SerializeField] public Collider2D      hitBoxCollider2D;
    int                                     howManyTimeDamagingEnemyIsCalled = 10;
    float                                   delayTimeBetweenDamage           = 0.5f; //en seconde
    [SerializeField]         float          currentModuleDamages;
    [SerializeField]         EnemyStat      enemyStat;
    [SerializeField]         SpriteRenderer spriteRenderer;
    [SerializeField]         EnemyMouvement enemyMouvement;
    [SerializeField] public  int            maxPropagationSteps;
    [SerializeField]         Color          baseColor;
    public Vector3                                 spawnPosition;
    [SerializeField] Light2D                electrocutionLight;
    public           PropagationCollider    propagationCollider;

    public void AddEnemy()
    {
        
    }
    public void PropagationSetup()
    {
        Debug.LogWarning(enemyStat.spawnPosition + " Propagation Started");
        PlayerStats.SINGLETON   = PlayerStats.SINGLETON;
        ModuleManager.SINGLETON = ModuleManager.SINGLETON;
        if (ModuleManager.SINGLETON.projectileAcquired == 1)
        {
            maxPropagationSteps = 2;
        }
        
        if (ModuleManager.SINGLETON.propagationAcquired == 2)
        {
            maxPropagationSteps = 4;
        }

        if (ModuleManager.SINGLETON.propagationAcquired == 3)
        {
            maxPropagationSteps = 6;
        }
        
        spawnPosition            = transform.position;
        
        if (enemyStat.hitBySword)
        {
            //Debug.LogWarning(spawnPosition + " Hit by sword");
        }
        
        //hitBoxCollider2D.enabled = true;
        DistanceBetweenEnemies();
        hitBoxCollider2D.enabled = false;
        //StartCoroutine(propagationCollider.CheckColliderActivation());
        //StartCoroutine(CallDamagingEnemyRepeatedly());
    }
    
    // Mesure la distance de chacun d'eux par rapport à l'ennemi initiateur de la propagation (en excluant ce dernier) et l'ajoute dans une liste.
	public void DistanceBetweenEnemies()
    {
        Debug.Log(spawnPosition + "Added Distances between enemies");
        //hitBoxCollider2D.enabled = false;
        distances.Clear();
        focusedEnemies.Remove(enemyStat);
        focusedEnemies.Remove(enemyStat.attacker);
        foreach (EnemyStat inFocus in focusedEnemies)
        {
            if (inFocus != null)
            {
                //Debug.Log(spawnPosition + " Added : " +  inFocus.name + " to the List.");
                distances.Add(Vector3.Distance(inFocus.transform.position, gameObject.transform.position));
            }
        }
        LookForSmallestDistance();
    }

    void LookForSmallestDistance() // Sélectionne la distance la plus petite dans la liste, correspondant à l'ennemi le plus proche de celui initiateur de la propagation.
    {
        if (distances.Count > 0)
        {
            Debug.Log(spawnPosition + "Looked for smallest distance");
            float minVal = distances.Min();
            shortestDistanceIndex = distances.IndexOf(minVal);
            //Debug.Log(spawnPosition + " INDEX : "        + shortestDistanceIndex);
            //Debug.Log(spawnPosition + " Electrocuted : " + enemyStat.isElectrocuted);
            TransmitToNextEnemy();
        }
        else
        { 
            Debug.Log(spawnPosition + " No enemy detected : Propagation Ended");
            EndPropagation();
        }
    }

    void TransmitToNextEnemy()
    {
        if (ModuleManager.SINGLETON.currentPropagationStep < maxPropagationSteps)
        {
            if (focusedEnemies[shortestDistanceIndex] != null && focusedEnemies[shortestDistanceIndex].CompareTag("Enemy") && focusedEnemies[shortestDistanceIndex].isElectrocuted == false)
            {
                Debug.LogWarning(spawnPosition + " Transmitted");
                ModuleManager.SINGLETON.currentPropagationStep++;
                focusedEnemies[shortestDistanceIndex].Propage();
                focusedEnemies[shortestDistanceIndex].attacker = enemyStat;
            }
            else
            {
                Debug.Log(spawnPosition + " Target Enemy was Dead");
                focusedEnemies.RemoveAt(shortestDistanceIndex);
                distances.RemoveAt(shortestDistanceIndex);
                LookForSmallestDistance();
            }
        }
        else
        {
            EndPropagation();
        }
    }


    public void EndPropagation()
    {
        ModuleManager.SINGLETON.currentPropagationStep = 0;
    }

    public IEnumerator CallDamagingEnemyRepeatedly()
    {
        hitBoxCollider2D.enabled = true;
        yield return new WaitForFixedUpdate();
        StartCoroutine(propagationCollider.Propagate());
        spriteRenderer.GetComponent<SpriteRenderer>().color = Color.yellow; // A CHANGER EN UN GLISSER DEPOSER
        enemyStat.isElectrocuted                            = true;
        electrocutionLight.enabled                          = true;
        for (int i = 0; i < howManyTimeDamagingEnemyIsCalled; i++)
        {
            enemyStat.TakeDamage(PlayerStats.SINGLETON.currentPlayerDamage / currentModuleDamages);   
            yield return new WaitForSeconds(delayTimeBetweenDamage); // attend X secondes
        }
        // Boucle terminée
        //Debug.Log(spawnPosition + " Boucle finie");
        spriteRenderer.GetComponent<SpriteRenderer>().color = baseColor; // PAREIL
        if (ModuleManager.SINGLETON.currentPropagationStep == maxPropagationSteps)
        {
            EndPropagation();
        }
        DisableCollider();
    }
    void DisableCollider()
    {
        Debug.Log(spawnPosition + "Collider Disabled");
        if (enemyStat.hitBySword)
        {
            enemyStat.TakeDamage(PlayerStats.SINGLETON.currentPlayerDamage);
        }
        enemyStat.HealthCheck();
        electrocutionLight.enabled       = false;
        enemyStat.isElectrocuted         = false;
        propagationCollider.enemiesAdded = false;
        focusedEnemies.Clear();
        distances.Clear();
        enemyMouvement.isStunned = false;
        //gameObject.SetActive(false);
    }
}
