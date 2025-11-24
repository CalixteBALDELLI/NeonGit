using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] playerWeapons;
    Canvas              weaponChoiceCanvas;
    
    [HideInInspector]
    public int          equippedWeapons;
    [HideInInspector]
    public int          weaponToEquip;
    [HideInInspector]
    public GameObject   pickedWeapon;


    public void WeaponEquiping()
    {
        if (equippedWeapons == 3)
        {
            
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
    }
}
