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
        public float spawnCount; //The number of enemies already spawnec in this wave
        
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

    Transform player;
    void Start()
    {
        PlayerMovement[] playerMovement = FindObjectsByType<PlayerMovement>(FindObjectsInactive.Exclude,FindObjectsSortMode.None); //IL FAUT CHANGER CA UNE FOIS LE PLAYER STATS FAIT <3
        player =  playerMovement[0].transform;
        CalculateWaveQuota();
        SpawnEnemies();
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
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota) ;
        {
            //Spawn each type of enemy until the quota is filled
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Check if the minimum of enemies of this type have been spawned
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                   Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10f, 10f), player.transform.position.y + Random.Range(-10f, 10f));
                   Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);
                   
                   enemyGroup.spawnCount++;
                   waves[currentWaveCount].spawnCount++;
                }
                
            }
        }

    }
    
}


