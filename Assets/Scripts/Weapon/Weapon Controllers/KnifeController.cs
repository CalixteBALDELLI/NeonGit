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
        GameObject spawnedKnife = Instantiate(weaponData.prefab); 
        spawnedKnife.transform.position = transform.position;  //Assign the position to be the same as this object which is parented to the player
        spawnedKnife.GetComponent<KnifeBehavior>().DirectionChecker(pm.lastMovedVector); //Reference and set the direction
    }
}
