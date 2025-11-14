using UnityEngine;

public class MapMenu : MonoBehaviour
{
    [SerializeField] GameObject mapMenuPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            mapMenuPanel.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            mapMenuPanel.SetActive(false);
        }
    }
}
