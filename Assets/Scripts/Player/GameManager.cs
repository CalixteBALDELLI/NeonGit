using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public float      playerHealth;
    public GameObject gameOverCanvas;
    public int        CurrentPlayerXP;
    void Update()
    {
        if (playerHealth <= 0)
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
