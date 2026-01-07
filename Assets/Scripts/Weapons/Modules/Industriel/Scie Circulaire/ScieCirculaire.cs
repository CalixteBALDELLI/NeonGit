using UnityEngine;

public class ScieCirculaire : MonoBehaviour
{
    public GameObject             scieCirculairePrefab;
    public WeaponScriptableObject circulaireData;
    public Transform              player;
    public float                  cooldown = 0.25f;

    void Start()
    {
        if(ModuleManager.SINGLETON.rebondAcquired >= 1)
        {
            CreateScie();
        }
    }

    public void CreateScie()
    {
        //yield return new WaitForSeconds(ModuleManager.SINGLETON.modulesData[23 + ModuleManager.SINGLETON.rebondAcquired].CooldownDuration);
        Instantiate(scieCirculairePrefab, player.position, Quaternion.identity);
        StartCoroutine(ModuleManager.SINGLETON.RebondCooldown(ModuleManager.SINGLETON.modulesData[42 + ModuleManager.SINGLETON.rebondAcquired].CooldownDuration));
    }
}

