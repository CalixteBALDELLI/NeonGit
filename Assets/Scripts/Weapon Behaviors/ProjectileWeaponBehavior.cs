using Unity.VisualScripting;
using UnityEngine;

public class ProjectileWeaponBehavior : MonoBehaviour
{

    protected Vector3 direction;
    public float destroyAfterSeconds;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    public void DirectionChecker(Vector3 dir) 
    {
        direction = dir; 

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //réglage de la rotation pour tous les armes de type projectile
        angle += -45;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
