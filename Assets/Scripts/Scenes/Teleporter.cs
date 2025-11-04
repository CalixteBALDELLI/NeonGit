using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    Canvas        mapChoiceCanvas;
    
    //GameObject       player;
    
    GameObject triggeredTeleporter;

    void Start()
    {
        //player = GameObject.Find("Player");
        mapChoiceCanvas = GameObject.Find("Map Choice").GetComponent<Canvas>();
    }
    public void OnTriggerEnter2D(Collider2D boxCollider)
    {
        //triggeredTeleporter     = gameObject;
        mapChoiceCanvas.enabled = true;
    }
}
