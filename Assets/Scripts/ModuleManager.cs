using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModuleManager : MonoBehaviour
{

    public bool propagationAcquired = true;


    // Variables module Propagation
    [SerializeField] GameObject       propagationStartingPoint; // Variable temporaire correpondant à l'ennemi ayant inité la propagation. Dans la version finalisée ce sera surement "gameobject".
    [SerializeField] List<float>      distances      = new List<float>();
    [SerializeField] List<GameObject> focusedEnemies = new List<GameObject>();
    int                               shortestDistanceIndex;
    bool                              propagationNotStarted = true;
    [SerializeField] Collider2D       hitBoxCollider2D;

    void Start()
    {
        hitBoxCollider2D.enabled = true;
    }
    
    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {
        if (other.CompareTag("Enemy"))
        {
            focusedEnemies.Add(other.gameObject);
        }
    }


    void Update()
    {
        if(propagationNotStarted)
        { 
            Propagation();
        }
    }
    public void Propagation()
    {
        {
            Debug.Log("Propagation");
            if (propagationAcquired)
            {
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
        propagationNotStarted = false;
    }
    void ChangeEnemyColor() // Changement de couleur représentant la prise de dégâts par l'ennemi
    {
        focusedEnemies[shortestDistanceIndex].GetComponent<SpriteRenderer>().color = Color.yellow; 
    }

    
    
    
    public void PropagationEffect()
    {
        Debug.Log("Met x degat par frame a l'ennemie et set la vitesse de l'enemie a 0.5");  
    }
    
    public void TirEnergie()
    {
        Debug.Log("tire energie");
    }
}
