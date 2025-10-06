using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject weapon70;
    public GameObject weapon30;
    public int whichweapon;
    private int spawnTime;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        whichweapon = Random.Range(0,101);
        spawnTime = Random.Range(5, 10);
        StartCoroutine(Weaponspawn());
    }

    // DEBUG, faire spawn l'arme en appuyant sur Espace
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        {
            //StartCoroutine(Weaponspawn());
        }
    }
    
    
    IEnumerator Weaponspawn()
    {
        yield return new WaitForSeconds(spawnTime);
        if (whichweapon <= 70)
        {
            Instantiate(weapon70, transform);
            Debug.Log("WhichWeapon? :" + whichweapon);
        }
        else
        {
            Instantiate(weapon30, transform);
            Debug.Log("WhichWeapon? :" + whichweapon);
        }
        Debug.Log("SpawnTime = " + spawnTime);
        
    }
}
