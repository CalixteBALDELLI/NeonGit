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
        StartCoroutine(ModuleManager.SINGLETON.ProjectileCooldown(3, 0.25f, 0.1f));
    }
    
    void Start()
    {
	    SpawnProjectile();
    }

    void Update()
    {
        
    }
}
