using System;
using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    [SerializeField] Canvas                  canvasComponent;

    public void DisableCanvas()
    {
        canvasComponent.enabled = false;
//        Debug.Log("COLLEC");
        if (PlayerStats.SINGLETON.hasLeveledUp == false)
        {
            Time.timeScale        = 1;
        }
    }
}
