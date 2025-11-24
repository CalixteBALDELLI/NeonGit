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
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        // centre de la caméra
        Vector3 camPos = cam.transform.position;

        // point aléatoire dans la zone visible
        float randomX = Random.Range(camPos.x - width , camPos.x + width );

        // Hauteur identique au joueur
        float randomY = Random.Range(camPos.x - height, camPos.x + height );

        Vector3 spawnPoint = new Vector3(randomX, randomY, 0f);

        Debug.Log("Point de spawn 2D : " + spawnPoint);

        // On spawn le préfab
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
