using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{

    [SerializeField] GameObject projectilePrefab;
    
	IEnumerator SpawnProjectile()
    {
        Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Debug.Log("Projectile spawned");
        yield return new WaitForSeconds(1f);
        StartCoroutine(SpawnProjectile());
    }
    
    void Start()
    {
       StartCoroutine(SpawnProjectile());
    }

    void Update()
    {
        
    }
}
