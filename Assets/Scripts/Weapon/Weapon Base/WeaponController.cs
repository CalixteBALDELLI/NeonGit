using UnityEngine;

public class WeaponCOntroller : MonoBehaviour
{
    [Header("Weapon Stats")] 
    public WeaponScriptableObject weaponData;
    float currentCooldown;


    protected PlayerMovement pm;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.cooldownDuration; //cooldown before starting to shoot
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        { 
            Attack();
        }
        
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.cooldownDuration;
    }
}
