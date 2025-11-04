using UnityEngine;

public class MakeDamage : MonoBehaviour
{
    HealthManager                gameManager;
    public EnemyScriptableObject enemyData;
    
    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.gameObject.tag == "Player")
        {
            gameManager              =  GameObject.Find("GameManager").GetComponent<HealthManager>();
            gameManager.playerHealth -= enemyData.Damage;
        }
        
    }
    
}
