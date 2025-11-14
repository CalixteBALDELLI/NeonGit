using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon" )]
public class WeaponScriptableObject :ScriptableObject
{
    public int weaponId;
    //Base stats for weapons
    [SerializeField]
    GameObject prefab; 
    public GameObject Prefab {get => prefab; private set => prefab = value; }
   
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }
    
    [SerializeField]
    public float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }
    
    [SerializeField]
    public int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }
    
    [SerializeField]
    string description;
    
    
}

