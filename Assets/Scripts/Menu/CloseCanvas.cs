using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    [SerializeField] Canvas                  canvasComponent;

    public void DisableCanvas()
    {
        Time.timeScale        = 1;
        canvasComponent.enabled = false;
        Debug.Log("COLLEC");
    }
    
}
