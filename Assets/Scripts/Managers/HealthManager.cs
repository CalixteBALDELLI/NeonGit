using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public           PlayerStats playerStats;
    public           Canvas      gameOverCanvas;
    public           Image       healthBar;
    [SerializeField] Canvas[]    canvasToDisableAtGameOver;

    
    GameObject        gameManager;
    void Start()
    {
        gameManager    = gameObject;
        //DontDestroyOnLoad(gameManager);
    }
    void Update()
    {
        if (playerStats.currentHealth <= 0)
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
