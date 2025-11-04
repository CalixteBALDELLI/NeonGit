using UnityEngine;

public class MeleeWeaponBehaviour : MonoBehaviour
{
     public WeaponScriptableObject WeaponData;
     
     public float destroyAfterSeconds;

     protected virtual void Start()
     {
          Destroy(gameObject, destroyAfterSeconds);
     }
     
}
