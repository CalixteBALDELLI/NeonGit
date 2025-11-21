using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Canvas        mapChoiceCanvas;
    
    //GameObject       player;
    
    GameObject                   triggeredTeleporter;
    [SerializeField] bool isOnFinalMap;
    [SerializeField] PlayerStats player;
    [SerializeField] Canvas      lockedMessage;
    [SerializeField] Canvas      victoryScreen;
    void Start()
    {
        //player = GameObject.Find("Player");
        mapChoiceCanvas = GameObject.Find("Map Choice").GetComponent<Canvas>();
    }
    public void OnTriggerEnter2D(Collider2D boxCollider)
    {
        if (PlayerStats.SINGLETON.teleporterKeyObtained)
        {
            if (isOnFinalMap)
            {
                victoryScreen.enabled = true;
                Time.timeScale = 0;
            }
            mapChoiceCanvas.enabled = true;
            Time.timeScale          = 0;
        }
        else
        { 
            lockedMessage.enabled = true;
            Time.timeScale        = 0;
        }
    }
}
