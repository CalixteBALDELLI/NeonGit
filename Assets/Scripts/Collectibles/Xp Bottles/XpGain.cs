using UnityEngine;

public class XpGain : MonoBehaviour
{
    PlayerStats                playerStats;
    public Bottle1ScriptableObject bottleData;
    
    
    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.gameObject.tag == "Player")
        {
            playerStats                 =  GameObject.Find("Player").GetComponent<PlayerStats>();
            playerStats.IncreaseExperience(bottleData.xpGift);
            Destroy(gameObject);
        }
    }
}

