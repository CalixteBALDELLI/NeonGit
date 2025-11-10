using System;
using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    Canvas                        canvas;
    public WeaponScriptableObject weaponData;
    public WeaponCollectibleData  weaponDataCollectible;
    InventoryManager              inventory;
    PlayerStats                   playerStats;
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerStats = other.GetComponent<PlayerStats>();
        canvas                   = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
        canvas.enabled           = true;
        inventory                = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        inventory.weaponToEquip  = weaponData.weaponId;
        playerStats.xpToExchange = weaponDataCollectible.xpValue;
        inventory.pickedWeapon   = gameObject;
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
