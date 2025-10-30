using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    
    public int          equippedWeapons;
    
    public GameObject[] playerWeapons;
    public int          weaponToEquip;
    Canvas              weaponChoiceCanvas;
    public GameObject   pickedWeapon;

    public void WeaponEquiping()
    {
        if (equippedWeapons == 3)
        {
            Debug.Log("Inventory Full");
        }
        else
        {
            weaponChoiceCanvas = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
            Debug.Log("equiped");
            playerWeapons[weaponToEquip].gameObject.SetActive(true);
            equippedWeapons++;
            weaponChoiceCanvas.enabled = false;
            Destroy(pickedWeapon);
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
