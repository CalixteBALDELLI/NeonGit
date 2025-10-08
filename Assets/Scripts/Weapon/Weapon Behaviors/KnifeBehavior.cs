using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior
{
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.Speed * Time.deltaTime; //set the movement of the knife
    }
}
