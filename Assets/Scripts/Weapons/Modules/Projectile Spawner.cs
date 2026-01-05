using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    [SerializeField] GameObject projectilePrefab;
    
	public void SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Debug.Log("Projectile spawned");
        //yield return new WaitForSeconds(1f);
        //StartCoroutine(SpawnProjectile());
        StartCoroutine(ModuleManager.SINGLETON.ProjectileCooldown(ModuleManager.SINGLETON.modulesData[ModuleManager.SINGLETON.projectileAcquired].CooldownDuration));
    }
    
    void Start()
    {
	    SpawnProjectile();
    }

    void Update()
    {
        
    }
}
