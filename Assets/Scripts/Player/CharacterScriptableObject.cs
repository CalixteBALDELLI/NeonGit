using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{
    public int currentHealth;
    
    [SerializeField]
    GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }
    
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    
    [SerializeField]
    float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }
    
    [SerializeField]
    float movingSpeed;
    public float MovingSpeed { get => movingSpeed; private set => movingSpeed = value; }
    
    [SerializeField]
    float might;
    public float Might { get => might; private set => might = value; }
    
    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }
    
    [SerializeField]
    float autoHealthRegeneration;
    public float AutoHealthRegeneration { get => autoHealthRegeneration; private set => autoHealthRegeneration = value; }
}

