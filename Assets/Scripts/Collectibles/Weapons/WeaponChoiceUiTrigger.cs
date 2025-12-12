using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    [SerializeField] Canvas                 weaponChoiceCanvas;
    [SerializeField] WeaponScriptableObject correspondingWeaponData;
    [SerializeField] WeaponCollectibleData  weaponCollectibleData;
    [SerializeField] WeaponChoiceTexts      weaponChoiceTexts;
    ModuleManager                           moduleManager = ModuleManager.SINGLETON;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
	    Debug.Log("Weapon Choice Trigger");
	   weaponChoiceCanvas                           = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
	    weaponChoiceTexts                            = weaponChoiceCanvas.GetComponent<WeaponChoiceTexts>();
	    Time.timeScale                               = 0;

	    weaponChoiceTexts.weaponNameText.text  = weaponCollectibleData.weaponName;
	    weaponChoiceTexts.descriptionText.text = weaponCollectibleData.description;
	    
	    if (correspondingWeaponData.isAnUpgrade && correspondingWeaponData.dealDamages)
	    {
		    weaponChoiceTexts.upgradesText[0].enabled = true;
		    weaponChoiceTexts.upgradesText[0].text = "Dégâts : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId - 1].Damage + " --) " + moduleManager.modulesData[correspondingWeaponData.weaponId].Damage;
	    }
	    if (correspondingWeaponData.isAnUpgrade && correspondingWeaponData.hasSpeed)
	    {
		    weaponChoiceTexts.upgradesText[1].enabled = true;
		    weaponChoiceTexts.upgradesText[1].text    = "Vitesse du projectile * " + moduleManager.modulesData[correspondingWeaponData.weaponId].Speed / moduleManager.modulesData[correspondingWeaponData.weaponId - 1].Speed;
	    }
	    if (correspondingWeaponData.isAnUpgrade && correspondingWeaponData.hasCooldown)
	    {
		    weaponChoiceTexts.upgradesText[2].enabled = true;
		    weaponChoiceTexts.upgradesText[2].text    = "Cooldown : " + moduleManager.modulesData[correspondingWeaponData.weaponId - 1].cooldownDuration + " --) " + moduleManager.modulesData[correspondingWeaponData.weaponId].cooldownDuration;
	    }
	    if (correspondingWeaponData.isAnUpgrade && correspondingWeaponData.hasPierce)
	    {
		    weaponChoiceTexts.upgradesText[3].enabled = true;
		    weaponChoiceTexts.upgradesText[3].text    = "Transpercions max. : " + moduleManager.modulesData[correspondingWeaponData.weaponId - 1].pierce + " --) " + moduleManager.modulesData[correspondingWeaponData.weaponId].pierce;
	    }
	    if (correspondingWeaponData.isAnUpgrade && correspondingWeaponData.hasKnockback)
	    {
		    weaponChoiceTexts.upgradesText[4].enabled = true;
		    weaponChoiceTexts.upgradesText[4].text    = "Force du recul * " + moduleManager.modulesData[correspondingWeaponData.weaponId].knockbackForce / moduleManager.modulesData[correspondingWeaponData.weaponId - 1].knockbackForce;
	    }
	    //weaponChoiceTexts.upgradeText.text     =;
	    //weaponChoiceTexts.damagesAndCooldownText.text = "Dégâts : " + weaponData.Damage             + " Cooldown : " + weaponData.cooldownDuration;
	    weaponChoiceTexts.xpGainText.text           = "+ " + weaponCollectibleData.xpValue + " XP";
	    weaponChoiceCanvas.enabled                  = true;
	    ModuleManager.SINGLETON.weaponToEquip       = weaponCollectibleData.weaponId;
	    PlayerStats.SINGLETON.xpToExchange          = weaponCollectibleData.xpValue;
	    ModuleManager.SINGLETON.pickedWeapon        = gameObject;
    }
}
