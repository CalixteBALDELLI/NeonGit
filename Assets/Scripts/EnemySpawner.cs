using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; //A list of groups of ennemies to spawn in this wave 
        public float waveQuota; //the total number of enemies to spawn in this wave
        public float spawnInterval;  // The interval at wich the enemy spawn
        public float spawnCount; //The number of enemies already spawned in this wave
        
    }
    
    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;  //the number of enemies to spawn in this wave
        public int spawnCount;  //the number of enemies already spawned in this wave
        public GameObject enemyPrefab;


    }
    
    public List<Wave> waves; //A list of all the list in the game
    public int currentWaveCount; //The Index of the current wave [start at 0]

    [Header("Spawner Attributes")]
    float spawnTimer; //Timer use to determine when to spawn the next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed; //The maximum of eemies allowed on the map at once
    public bool maxEnemiesReached = false; //A flag indicating if the maximum number of enemies has been reached
    public float waveInterval; //The interval between each wave
    
    [Header("Spawn Position")]
    public List<Transform> relativeSpawnPoints; //A list to strore all the relative spawn points of enemies 
        
        
        
    Transform player;
    void Start()
    {
        PlayerMovement[] playerMovement = FindObjectsByType<PlayerMovement>(FindObjectsInactive.Exclude,FindObjectsSortMode.None); //IL FAUT CHANGER CA UNE FOIS LE PLAYER STATS FAIT <3
        player =  playerMovement[0].transform;
        CalculateWaveQuota();
        
    }

    void Update()
    {
        
       if (currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) // Check if the wave has ended and the next wave should sart
       {
           StartCoroutine(BeginNextWave());
       }
       spawnTimer += Time.deltaTime;

        //Check if it's time to  spawn the next enemy
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;   
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        //Wave for 'waveInterval' seconds before starting the next wave
        yield return new WaitForSeconds(waveInterval);
        
        
        if (currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }
    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        //Check if the minimum number of enemies in the wave have been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Check if the minimum of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    //Limit the number of enemies that can be spawned at once
                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                    //Spawn the enemy at a random position close to the player 
                    Instantiate(enemyGroup.enemyPrefab,
                        player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position,
                        Quaternion.identity);
                   
                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                } 
                
            }
        }
        //reset the maxenemiesreached flag
        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached  = false;
        }

    }
    // Call this function when an enemy is killed
    public void OnEnemyKilled()
    {
        //Decrement the number of enemies alive
        enemiesAlive--;
    }
    
}


