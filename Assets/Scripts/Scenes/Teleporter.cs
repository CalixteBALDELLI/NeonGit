using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Canvas        mapChoiceCanvas;
    
    //GameObject       player;
    
    GameObject   triggeredTeleporter;
    [SerializeField] PlayerStats player;
    [SerializeField] Canvas lockedMessage;
    void Start()
    {
        //player = GameObject.Find("Player");
        mapChoiceCanvas = GameObject.Find("Map Choice").GetComponent<Canvas>();
    }
    public void OnTriggerEnter2D(Collider2D boxCollider)
    {
        if (player.teleporterKeyObtained)
        {
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
