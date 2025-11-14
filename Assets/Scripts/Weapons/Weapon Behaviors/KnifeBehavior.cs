using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior
{
    ModuleManager moduleManager;
    protected override void Start()
    {
        moduleManager = FindObjectOfType<ModuleManager>();
        if (moduleManager.projectileAcquired == 2)
        {
            weaponData = moduleManager.projectileLvl2;
        }
        else if (moduleManager.projectileAcquired == 3)
        {
            weaponData = moduleManager.projectileLvl3;
        }
        RefreshStats();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime; //set the movement of the knife
    }
}
