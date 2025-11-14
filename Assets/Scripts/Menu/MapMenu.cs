using UnityEngine;

public class MapMenu : MonoBehaviour
{
    [SerializeField] GameObject mapMenuPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (mapMenuPanel.activeSelf == true)
            {
                mapMenuPanel.SetActive(false);
                Time.timeScale = 1;
            }
            else if (mapMenuPanel.activeSelf == false)
            {
                mapMenuPanel.SetActive(true);
                Time.timeScale = 0;
            }
            
            
        }
        
       
    }
}
