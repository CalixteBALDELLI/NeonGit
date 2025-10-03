using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    
    protected Vector3 direction;
    public float destroyAfterSeconds;
    
    //Curent stat
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    
    void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir) 
    {
        direction = dir; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //réglage de la rotation pour tous les armes de type projectile
        angle += -45;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {   
		Debug.Log(0);
        //Reference the script from the collider and deal damage using TakeDamage
        if (col.CompareTag("Enemy"))
        {
            EnemyStat enemy = col.GetComponent<EnemyStat>();
            enemy.TakeDamage(currentDamage); //make sur to use currentDamage instead of weaponData.Damage in case of damage multiplier in the future
        }
    }
}
