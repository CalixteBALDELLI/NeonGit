using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting.Dependencies.NCalc;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class RelativePosition : MonoBehaviour
{
    // Variables module Propagation
    [SerializeField] List<float>      distances      = new List<float>();
    [SerializeField] List<GameObject> focusedEnemies = new List<GameObject>();
    int                               shortestDistanceIndex;
    bool                              propagationNotStarted = true;
    [SerializeField] Collider2D       hitBoxCollider2D;
    [SerializeField] ModuleManager    moduleManager = FindAnyObjectByType<ModuleManager>();
    int                               howManyTimeDamagingEnemyIsCalled = 10;
    float                             delayTimeBetweenDamage = 2f; //en seconde
    
    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {
        if (other.CompareTag("Enemy"))
        {
            focusedEnemies.Add(other.gameObject);
        }
    }

    
    void Update()
    {
        
        if (moduleManager.propagationAcquired)
        {
            hitBoxCollider2D.enabled = true; // Activation de la HitBox une fois le module Propagation obtenu.

        }
        if(propagationNotStarted) // Si une propagation n'a pas déjà eu lieu, démarrage d'une propagation (dans Update(); sinon elle démarre avant que tous les ennemis dans le collider n'aient été listés)..
        { 
            Propagation();
        }
    }
    public void Propagation()
    {
        Debug.Log("Propagation");
        if (propagationNotStarted)
        {
            StartCoroutine(CallDamagingEnemyRepeatedly());
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
    }

    
    public void DistanceBetweenEnemies() // Mesure la distance de chacun d'eux par rapport à l'ennemi initiateur de la propagation (en excluant ce dernier) et l'ajoute dans une liste.
    {
        Debug.Log("Distance between enemies");
        focusedEnemies.Remove(gameObject);
        foreach (GameObject inFocus in focusedEnemies)
        {
            distances.Add(Vector3.Distance(inFocus.transform.position, gameObject.transform.position));
        }
        LookForSmallestDistance();
    }
    
    void LookForSmallestDistance() // Sélectionne la distance la plus petite dans la liste, correspondant à l'ennemi le plus proche de celui initiateur de la propagation.
    {
        Debug.Log("Looking for smallest distance");
        float minVal      = distances.Min();
        shortestDistanceIndex = distances.IndexOf(minVal);
        ChangeEnemyColor();
        propagationNotStarted = false; // marque la propagation comme terminée, pour éviter qu'une autre ne se déclenche automatiquement.
    }
    void ChangeEnemyColor() // Changement de couleur représentant la prise de dégâts par l'ennemi
    {
        focusedEnemies[shortestDistanceIndex].GetComponent<SpriteRenderer>().color = Color.yellow;   
    }

    IEnumerator CallDamagingEnemyRepeatedly()
    {
        var dataEnemy = EnemyStat.instance;
        
        
        for (int i = 0; i < howManyTimeDamagingEnemyIsCalled; i++)
        {
            Debug.Log(dataEnemy.currentHealth);
            DamagingEnemy();

            if (dataEnemy.currentHealth <= 0)
            {
                Debug.Log("Ennemi vaincu");
                yield break; 
            }

            yield return new WaitForSeconds(delayTimeBetweenDamage); // attend X secondes
        }
        Debug.Log("Boucle fini");
    }
    void DamagingEnemy()
    {
        var dataPlayer = PlayerStats.instance;
        var dataEnemy = EnemyStat.instance;
        dataEnemy.currentHealth -= dataPlayer.currentPlayerDamage / dataPlayer.currentModulesDamages;
    }
    
    
}
