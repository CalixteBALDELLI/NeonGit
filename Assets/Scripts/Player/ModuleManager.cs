using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModuleManager : MonoBehaviour
{
    [SerializeField]        KnifeController projectileController;
    [SerializeField] public bool            propagationAcquired;
    [SerializeField] public bool            knockbackAcquired;

    public EnemyMouvement enemyMovement;
    
    
    

    void ActivateProjectile()
    {
        projectileController.enabled = true;
    }

    void ActivatePropagation()
    {
        propagationAcquired = true;
    }
    
    public IEnumerator Knockback()
    {
        if (knockbackAcquired)
        {
            Debug.Log("Knockback");
            enemyMovement.isKnockedBack = true;
            yield return new WaitForSeconds(0.5f);
            enemyMovement.isKnockedBack     = false;
        }
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
