using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public           Canvas      gameOverCanvas;
    [SerializeField] Canvas[]    canvasToDisableAtGameOver;

    
    void Update()
    {
        if (PlayerStats.SINGLETON.currentHealth <= 0)
        {
            foreach (var canvas in canvasToDisableAtGameOver)
            {
                enabled = false;
            }
            gameOverCanvas.enabled = true;
            Time.timeScale = 0;
        }
    }

    public void Restart()
    { 
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
}
