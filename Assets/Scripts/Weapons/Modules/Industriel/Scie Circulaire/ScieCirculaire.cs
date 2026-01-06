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

    public IEnumerator CreateScie()
    {
        while (ModuleManager.SINGLETON.rebondAcquired >= 1)
        {
            yield return new WaitForSeconds(ModuleManager.SINGLETON.modulesData[23 + ModuleManager.SINGLETON.rebondAcquired].CooldownDuration);
            Instantiate(scieCirculairePrefab, player.position, Quaternion.identity);
        }
        
    }
}
