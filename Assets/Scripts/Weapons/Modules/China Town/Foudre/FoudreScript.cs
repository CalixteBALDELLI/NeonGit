using System.Collections;
using UnityEngine;

public class FoudreScript : MonoBehaviour
{
    public Transform                player;
    public GameObject               hitboxPrefab;
    public float                    timeUntilNextAoe;
    Camera                          cam;
    public WeaponScriptableObject[] foudreData;

    void Start()
    {
        cam = Camera.main;
        if (ModuleManager.SINGLETON.foudreAcquired > 0)
        {
            HitZone();
            StartCoroutine(Delay());
        }
    }

    
    public void HitZone()
    {
        // tailles en unité monde
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        // centre caméra
        Vector3 camPos = cam.transform.position;

        // Point aléatoire dans la zone visible
        float randomX = Random.Range(camPos.x - halfWidth, camPos.x + halfWidth);
        float randomY = Random.Range(camPos.y - halfHeight, camPos.y + halfHeight);

        Vector3 spawnPoint = new Vector3(randomX, randomY, 0f);

        Debug.Log("Point de spawn 2D : " + spawnPoint);

        Instantiate(hitboxPrefab, spawnPoint, Quaternion.identity);
    }

    public IEnumerator Delay()
    {
        timeUntilNextAoe = foudreData[ModuleManager.SINGLETON.foudreAcquired - 1].cooldownDuration;
        while (ModuleManager.SINGLETON.foudreAcquired > 0)
        {
            yield return new WaitForSeconds(timeUntilNextAoe);
            HitZone();
        }


    }
    
    
}
