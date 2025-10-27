using UnityEngine;
using UnityEngine.UI;

public class WeaponEquip : MonoBehaviour
{
    
    public Button                 button;
    public WeaponScriptableObject weaponData;
    public GameObject            inventory;
    public GameObject            weaponController;
    public GameObject            player;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button.onClick.AddListener(WeaponEquiping);
    }

    
    public void WeaponEquiping()
    {
        //if (inventory.equippedWeapons == 3)
        {
            Debug.Log("Inventory Full");
        }
        //else
        {
            Instantiate(weaponController, transform, player);
            //inventory.equippedWeapons ++;
            Debug.Log("equiped");
        }
        
        
    }
    private void OnDestroy()
    {
        button.onClick.RemoveListener(WeaponEquiping);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
