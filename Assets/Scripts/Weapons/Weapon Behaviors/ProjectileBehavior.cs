using UnityEngine;

public class ProjectileBehavior : ProjectileWeaponBehavior
{
    ModuleManager moduleManager;
    protected override void Start()
    {
        moduleManager = ModuleManager.SINGLETON;
        if (moduleManager.projectileAcquired == 2)
        {
            weaponData = moduleManager.modulesData[1];
        }
        else if (moduleManager.projectileAcquired == 3)
        {
            weaponData = moduleManager.modulesData[2];
        }
        RefreshStats();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime; //set the movement of the knife
    }
}
