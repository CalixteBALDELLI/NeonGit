using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ModuleManager : MonoBehaviour
{
    [SerializeField] KnifeController      projectileController;
    [SerializeField] GameObject projectileControllerGameObject;

    [SerializeField] public bool       propagationAcquired;
    [SerializeField] public bool       knockbackAcquired;
    [SerializeField] public bool       projectileAcquired;
    
    [HideInInspector]
    public int          weaponToEquip;
    [HideInInspector]
    public GameObject   pickedWeapon;
    [HideInInspector]
    public int          equippedWeapons;
    Canvas              weaponChoiceCanvas;

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
            if (weaponToEquip == 0)
            {
                projectileAcquired  = true;
                projectileControllerGameObject.SetActive(true);
            }
            else if (weaponToEquip == 1)
            {
                knockbackAcquired = true;
            }
            else if (weaponToEquip == 2)
            {
                propagationAcquired = true;
            }

            equippedWeapons++;
            weaponChoiceCanvas.enabled = false;
            Destroy(pickedWeapon);
        }
    }
    
    
    
    
    
    
    public void PropagationEffect()
    {
        Debug.Log("Met x degat par frame a l'ennemie et set la vitesse de l'enemie a 0.5");  
    }

    public void TirEnergie()
    {
        Debug.Log("tire energie");
    }
}
