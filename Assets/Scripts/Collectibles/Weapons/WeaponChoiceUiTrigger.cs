using System;
using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    Canvas                                  canvas;
    [SerializeField] WeaponScriptableObject weaponData;
    [SerializeField] WeaponCollectibleData  weaponCollectibleData;
    ModuleManager                           moduleManager;
    PlayerStats                             playerStats;
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerStats                 = GameObject.Find("Player").GetComponent<PlayerStats>();
        canvas                      = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
        moduleManager               = GameObject.Find("GameManager").GetComponent<ModuleManager>();
        canvas.enabled              = true;
        moduleManager.weaponToEquip = weaponCollectibleData.weaponId;
        playerStats.xpToExchange    = weaponCollectibleData.xpValue;
        moduleManager.pickedWeapon  = gameObject;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        //button.onClick.AddListener(WeaponEquiping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
