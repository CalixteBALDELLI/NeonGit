using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject  gameOverCanvas;
    public Image healthBar;
    
    
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

        UpdateHealthBar();

    }

    void UpdateHealthBar()
    {
        throw new System.NotImplementedException();
    }

    public void Restart()
    { 
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    
}
