using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    [SerializeField] KnifeController      projectileController;
    [SerializeField] GameObject projectileControllerGameObject;

    [SerializeField] public int propagationAcquired;
    [SerializeField] public int knockbackAcquired;
    [SerializeField] public int projectileAcquired;

    [SerializeField] public WeaponScriptableObject projectileLvl2;
    [SerializeField] public WeaponScriptableObject projectileLvl3;

    [SerializeField] public WeaponScriptableObject knockbackLvl2;
    [SerializeField] public WeaponScriptableObject knockbackLvl3;
    
    [SerializeField] KnifeController weaponController;
    [SerializeField] Canvas          inventoryFullMessage;

    public bool propagationInProgress;
    public int  currentPropagationStep;

    
    [HideInInspector]
    public int          weaponToEquip;
    [HideInInspector]
    public GameObject   pickedWeapon;
    [HideInInspector]
    public int          equippedWeapons;
    Canvas              weaponChoiceCanvas;

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
            }
            else if (weaponToEquip == 0 && projectileAcquired == 1 || projectileAcquired == 2)
            {
                projectileAcquired++;
                if (projectileAcquired == 2)
                {
                    weaponController.weaponData = projectileLvl2;
                }
                else if (projectileAcquired == 3)
                {
                    weaponController.weaponData = projectileLvl3;
                }
            }
            
            
            if (weaponToEquip == 1)
            {
                knockbackAcquired++;
            }
            
            if (weaponToEquip == 2)
            {
                propagationAcquired++;
            }


            equippedWeapons++;
            weaponChoiceCanvas.enabled = false;
            Time.timeScale             = 1;
            Destroy(pickedWeapon);
        }
    }
    
    

    public void TirEnergie()
    {
        Debug.Log("tire energie");
    }
}
