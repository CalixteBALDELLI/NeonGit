using UnityEngine;

public class XpGain : MonoBehaviour
{
    XpManager                  gameManager;
    public Bottle1ScriptableObject bottleData;
    
    public void OnTriggerEnter2D(Collider2D cl2D)
    {
        if (cl2D.gameObject.tag == "Player")
        {
            gameManager                 =  GameObject.Find("GameManager").GetComponent<XpManager>();
            gameManager.currentPlayerXP += bottleData.xpGift;
            Destroy(gameObject);
        }
    }
}

