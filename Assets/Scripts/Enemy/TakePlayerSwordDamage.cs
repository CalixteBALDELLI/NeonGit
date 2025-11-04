using System;
using Unity.VisualScripting;
using UnityEngine;

public class TakePlayerSwordDamage : MonoBehaviour
{
    
    public EnemyStat              ennemyStats;
    public WeaponScriptableObject playerSword;

    
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "PlayerSword")
        {
            ennemyStats.TakeDamage(playerSword.Damage);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
