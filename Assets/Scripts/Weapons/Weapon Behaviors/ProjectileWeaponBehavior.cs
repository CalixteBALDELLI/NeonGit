using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    protected Vector3 direction;
    public float destroyAfterSeconds;
    
    //Curent stat
    [SerializeField] protected float currentDamage;
    [SerializeField] protected int   currentPierce;

    void Awake()
    {
	    RefreshStats();
    }

    public void RefreshStats()
    {
        currentDamage           = weaponData.Damage;
	    currentPierce           = weaponData.Pierce;
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
        //Reference the script from the collider and deal damage using TakeDamage
        if (col.CompareTag("Enemy"))
        {
            EnemyStat enemy = col.GetComponent<EnemyStat>();
            enemy.TakeDamage(currentDamage); //make sur to use currentDamage instead of weaponData.Damage in case of damage multiplier in the future
        	ReducePierce();
// 	        Debug.Log(currentPierce);
		}
	}

	void ReducePierce() //Destroy once the pierce peaches 0
	{
		currentPierce--;
		if(currentPierce <= 0)
		{
			Destroy(gameObject);
				
		}
			
	}	

}
