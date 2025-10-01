
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //base stats for ennemies
    
    [SerializeField]
    float moveSpeed; 
    public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value; }
    
    [SerializeField]
    float maxHealth;
    public float MaxHealth {get => MaxHealth; private set => MaxHealth = value; }
   
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value; }
    
}
