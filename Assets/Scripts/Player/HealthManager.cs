using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject  gameOverCanvas;
    
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
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Restart()
    { 
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
