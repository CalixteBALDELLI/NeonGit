using UnityEngine;

public class CloseCanvas : MonoBehaviour
{
    [SerializeField] Canvas                  canvasComponent;

    public void DisableCanvas()
    {
        Debug.Log ("CanvasClosed");
        canvasComponent.enabled = false;
    }
    
}
