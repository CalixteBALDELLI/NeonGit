using UnityEngine;

public class TeleporterKey : MonoBehaviour
{
    PlayerStats  player;
    EnemySpawner enemySpawner;
    
    void OnTriggerEnter2D(Collider2D collider)
    {
	    Debug.Log(collider.name);
	    player                        = GameObject.Find("Player").GetComponent<PlayerStats>();
	    enemySpawner                  = FindAnyObjectByType<EnemySpawner>();
	    player.teleporterKeyObtained  = true;
	    enemySpawner.currentWaveCount = 4;
	    Destroy(gameObject);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
