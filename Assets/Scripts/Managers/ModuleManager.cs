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
    
    [SerializeField]        KnifeController       weaponController;

    
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
