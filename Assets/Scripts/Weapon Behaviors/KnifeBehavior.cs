using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior
{
    private KnifeController kc;
    protected override void Start()
    {
        base.Start();
        kc = FindObjectOfType<KnifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * kc.speed * Time.deltaTime;
    }
}
