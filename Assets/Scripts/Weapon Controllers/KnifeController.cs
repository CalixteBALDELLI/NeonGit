using UnityEngine;

public class KnifeController : WeaponCOntroller
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack(); // Spawn avec réglage de la direction du couteau
        GameObject spawnedKnife = Instantiate(prefab); 
        spawnedKnife.transform.position = transform.position;
        spawnedKnife.GetComponent<KnifeBehavior>().DirectionChecker(pm.lastMovedVector);
    }
}
