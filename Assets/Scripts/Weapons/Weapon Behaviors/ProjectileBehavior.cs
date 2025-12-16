using UnityEngine;

public class ProjectileBehavior : ProjectileWeaponBehavior
{
    protected override void Start()
    {
        if (ModuleManager.SINGLETON.projectileAcquired == 2)
        {
            weaponData = ModuleManager.SINGLETON.modulesData[1];
        }
        else if (ModuleManager.SINGLETON.projectileAcquired == 3)
        {
            weaponData = ModuleManager.SINGLETON.modulesData[2];
        }
        RefreshStats();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime; //set the movement of the knife
    }
}
