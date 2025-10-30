using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    
    public  int        whichweapon;
    private int        spawnTime;
    public  MapData    mapData;
    public  int        minSpawnTime;
    public  int        maxSpawnTime;
    
    void Start()
    {
        StartCoroutine(Weaponspawn());
    }

    // DEBUG, faire spawn l'arme en appuyant sur Espace
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Weaponspawn());
        }
    }
    
    IEnumerator Weaponspawn()
    {
        whichweapon = Random.Range(0,            mapData.playerWeapons.Length);
        spawnTime   = Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(spawnTime);
        Instantiate(mapData.playerWeapons[whichweapon], transform);
    }
    
}
