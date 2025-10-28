using UnityEngine;

public class XpGain : MonoBehaviour
{
    HealthManager                  gameManager;
    public Bottle1ScriptableObject bottleData;
    
    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.gameObject.tag == "Player")
        {
            gameManager                 =  GameObject.Find("GameManager").GetComponent<HealthManager>();
            gameManager.CurrentPlayerXP += bottleData.xpGift;
            Destroy(gameObject);
        }
    }
}

