
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    //base stats for ennemies
    public float moveSpeed;
    public float maxHealth;
    public float damage;
    
    
}
