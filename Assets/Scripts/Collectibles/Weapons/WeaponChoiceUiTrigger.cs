using System;
using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    Canvas             canvas;
    public WeaponScriptableObject weaponData;
    InventoryManager              inventory;
    private void OnTriggerEnter2D(Collider2D other)
    {
        canvas = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
        canvas.enabled = true;
        inventory               = GameObject.Find("GameManager").GetComponent<InventoryManager>();
        inventory.weaponToEquip = weaponData.weaponId;
        inventory.pickedWeapon = gameObject;
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
