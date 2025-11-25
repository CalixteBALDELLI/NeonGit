using System;
using System.Collections;
using UnityEngine;

public class Saignement : MonoBehaviour
{
    public float speedDamageSaignement = 0.25f;

    private void Start()
    {
        StartCoroutine(DamageEnemiesSaignement());
    }

    IEnumerator DamageEnemiesSaignement()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(speedDamageSaignement);
        }
            
            
        
        
    }
    
}
