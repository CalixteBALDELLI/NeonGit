using System.Collections;
using UnityEngine;

public class ScieCirculaire : MonoBehaviour
{
    public GameObject scieCirculairePrefab;
    public Transform player;
    public float cooldown = 0.25f;

    void Start()
    {
        StartCoroutine(CreateScie());
    }

    IEnumerator CreateScie()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            Instantiate(scieCirculairePrefab, player.position, Quaternion.identity);
        }
        
    }
}
