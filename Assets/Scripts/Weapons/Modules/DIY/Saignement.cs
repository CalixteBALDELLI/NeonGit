using System;
using System.Collections;
using UnityEngine;

public class Saignement : MonoBehaviour
{
    [SerializeField]         EnemyStat      enemyStat;
    public float speedDamageSaignement = 0.125f;
    public float damageSaignement = 0.05f;
    private bool isBleeding = false; 
    
    

    public void CallSaignememnt()
    {
        if (isBleeding == false)
        {
            isBleeding =  true;
            StartCoroutine(DamageEnemiesSaignement());
        }
        
    }

    IEnumerator DamageEnemiesSaignement()
    {
        while (true)
        {
            enemyStat.TakeDamage(damageSaignement);
            yield return new WaitForSeconds(speedDamageSaignement);
        }
            
            
        
        
    }
    
}
