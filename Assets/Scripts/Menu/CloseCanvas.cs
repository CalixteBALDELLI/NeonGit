using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    [SerializeField] Canvas                  canvasComponent;

    public void DisableCanvas()
    {
        canvasComponent.enabled = false;
        Debug.Log("COLLEC");
        Time.timeScale        = 1;
    }
    
}
