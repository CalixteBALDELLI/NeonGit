using System.Collections;
using UnityEngine;

public class FoudreScript : MonoBehaviour
{
    public GameObject hitboxPrefab;
    public float timeUntilNextAoe = 0f;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
        //HitZone();
    }

    
    void HitZone()
    {
        float x = Random.value;
        float y = Random.value;

        Vector3 randomPoint = cam.ViewportToWorldPoint(new Vector3(x, y, cam.nearClipPlane));
        randomPoint.y = 0f;

        Debug.Log("Point aléatoire : " + randomPoint);

        // Spawn hitbox
        Instantiate(hitboxPrefab, randomPoint, Quaternion.identity);
    }

    public IEnumerator Delay()
    {
        Debug.unityLogger.Log("Delay");
        yield return new WaitForSeconds(timeUntilNextAoe);
        
    }
    
    
}
