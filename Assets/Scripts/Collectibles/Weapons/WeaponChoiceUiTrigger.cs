using System;
using TMPro;
using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    Canvas                                  weaponChoiceCanvas;
    [SerializeField] WeaponScriptableObject weaponData;
    [SerializeField] WeaponCollectibleData  weaponCollectibleData;
    ModuleManager                           moduleManager;
    PlayerStats                             playerStats;
    WeaponChoiceTexts                       weaponChoiceTexts;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        playerStats                            = GameObject.Find("Player").GetComponent<PlayerStats>();
        moduleManager                          = GameObject.Find("GameManager").GetComponent<ModuleManager>();
        weaponChoiceCanvas                     = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
        weaponChoiceTexts                      = weaponChoiceCanvas.GetComponent<WeaponChoiceTexts>();
        weaponChoiceTexts.weaponNameText.text  = weaponCollectibleData.weaponName;
        weaponChoiceTexts.descriptionText.text = weaponCollectibleData.description;
        //weaponChoiceTexts.upgradeText.text     = weaponCollectibleData;
        weaponChoiceTexts.damagesAndCooldownText.text = "Dégâts : " + weaponData.Damage             + " Cooldown : " + weaponData.cooldownDuration;
        weaponChoiceTexts.xpGainText.text             = "+ "        + weaponCollectibleData.xpValue + " XP";
        weaponChoiceCanvas.enabled             = true;
        moduleManager.weaponToEquip            = weaponCollectibleData.weaponId;
        playerStats.xpToExchange               = weaponCollectibleData.xpValue;
        moduleManager.pickedWeapon             = gameObject;

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
