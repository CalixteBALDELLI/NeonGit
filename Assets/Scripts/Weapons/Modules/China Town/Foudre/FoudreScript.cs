using System.Collections;
using UnityEngine;

public class FoudreScript : MonoBehaviour
{
    public Transform player;
    public GameObject hitboxPrefab;
    public float timeUntilNextAoe = 2f;
    Camera cam;
    public bool foudreAcquired = true;

    void Start()
    {
        cam = Camera.main;
        HitZone();
        StartCoroutine(Delay());
    }

    
    void HitZone()
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
        while (foudreAcquired == true)
        {
            yield return new WaitForSeconds(timeUntilNextAoe);
            HitZone();
        }


    }
    
    
}
