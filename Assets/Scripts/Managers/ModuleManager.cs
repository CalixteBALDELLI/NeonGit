using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleManager : MonoBehaviour
{
    public static ModuleManager SINGLETON;
    [SerializeField] ProjectileController      projectileController;
    [SerializeField] GameObject projectileControllerGameObject;

    [SerializeField] public int propagationAcquired;
    [SerializeField] public int knockbackAcquired;
    [SerializeField] public int projectileAcquired;

    [SerializeField] public WeaponScriptableObject projectileLvl2;
    [SerializeField] public WeaponScriptableObject projectileLvl3;

    [SerializeField] public WeaponScriptableObject knockbackLvl2;
    [SerializeField] public WeaponScriptableObject knockbackLvl3;
    
    [SerializeField] ProjectileController weaponController;
    [SerializeField] Canvas               inventoryFullMessage;

    public bool propagationInProgress;
    public int  currentPropagationStep;

    [SerializeField] Image[] inventoryIcons;
    [SerializeField] Image[] inventoryBackgrounds;
    int                      weaponIconIndex;
    
    [HideInInspector] public int        weaponToEquip;
    [HideInInspector] public Sprite     weaponToEquipSprite;
    [HideInInspector] public GameObject pickedWeapon;
    [HideInInspector] public int        equippedWeapons;
    Canvas                              weaponChoiceCanvas;

    void Awake()
    {
        if (SINGLETON == null)
        {
            SINGLETON = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void WeaponEquiping()
    {
        if (equippedWeapons == 6)
        {
            inventoryFullMessage.enabled = true;        
        }
        else
        {
            weaponChoiceCanvas = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
            if (weaponToEquip == 0 && projectileAcquired == 0)
            {
                projectileAcquired++;
                projectileControllerGameObject.SetActive(true);
                InventoryUiUpdate();
            }
            else if (weaponToEquip == 0 && projectileAcquired == 1 || projectileAcquired == 2)
            {
                projectileAcquired++;
                if (projectileAcquired == 2)
                {
                    weaponController.weaponData = projectileLvl2;
                    InventoryUiUpdate();
                }
                else if (projectileAcquired == 3)
                {
                    weaponController.weaponData = projectileLvl3;
                    InventoryUiUpdate();
                }
            }
            
            
            if (weaponToEquip == 1)
            {
                knockbackAcquired++;
                InventoryUiUpdate();

            }
            
            if (weaponToEquip == 2)
            {
                propagationAcquired++;
                InventoryUiUpdate();
            }


            equippedWeapons++;
            weaponChoiceCanvas.enabled = false;
            Time.timeScale             = 1;
            Destroy(pickedWeapon);
        }
    }

    void InventoryUiUpdate()
    {
        inventoryIcons[weaponIconIndex].sprite  = weaponToEquipSprite;
        inventoryIcons[weaponIconIndex].enabled = true;
        weaponIconIndex++;
        Debug.Log(weaponIconIndex);
    }

    public void TirEnergie()
    {
        Debug.Log("tire energie");
    }
}
