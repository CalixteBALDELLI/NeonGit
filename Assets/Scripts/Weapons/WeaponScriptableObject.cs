using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon" )]
public class WeaponScriptableObject :ScriptableObject
{
    public int    weaponId;
    public string weaponName;
    public string description;
    public int    xpValue;
    public Sprite weaponIcon;
    public bool isAnUpgrade;
    //Base stats for weapons
    [Header("Module Type")]
    public bool isPropagation;
    public bool isFoudre;
    public bool isProjectile;
    public bool isSaignement;
    public bool isKnockback;
    
    
    [SerializeField]
    GameObject prefab; 
    public GameObject Prefab {get => prefab; private set => prefab = value; }
   
    public bool dealDamages;
    public bool isAffliction;
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    public bool hasSpeed;
    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    public bool hasCooldown;
    [SerializeField]
    public float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    public bool hasPierce;
    [SerializeField]
    public int pierce;
    public int Pierce { get => pierce; private set => pierce = value; }

    public bool  hasKnockback;
    public float knockbackForce;
    public int   maxKnockBackSteps;
    public bool  hasDuration;
    public float duration;
}

