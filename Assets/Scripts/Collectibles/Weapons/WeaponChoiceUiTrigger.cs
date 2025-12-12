using System.Reflection;
using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    [SerializeField] Canvas                 weaponChoiceCanvas;
    [SerializeField] WeaponScriptableObject correspondingWeaponData;
    [SerializeField] WeaponCollectibleData  weaponCollectibleData;
    [SerializeField] WeaponChoiceTexts      weaponChoiceTexts;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
	    Debug.Log("Weapon Choice Trigger");
	    weaponChoiceCanvas                           = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
	    weaponChoiceTexts                            = weaponChoiceCanvas.GetComponent<WeaponChoiceTexts>();
	    Time.timeScale                               = 0;

	    weaponChoiceTexts.weaponNameText.text  = weaponCollectibleData.weaponName;
	    weaponChoiceTexts.descriptionText.text = weaponCollectibleData.description;
	    
	    if (correspondingWeaponData.dealDamages)
	    {
		    weaponChoiceTexts.upgradesText[0].enabled = true;
		    if (correspondingWeaponData.isAnUpgrade)
		    {
			    weaponChoiceTexts.upgradesText[0].text = ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId - 1].Damage + " --) " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].Damage;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[0].text = "Dégâts : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].Damage;
		    }
	    }
	    if (correspondingWeaponData.hasSpeed)
	    {
		    weaponChoiceTexts.upgradesText[1].enabled = true;
		    if (correspondingWeaponData.isAnUpgrade)
		    {
			    weaponChoiceTexts.upgradesText[1].text    = "Vitesse du projectile * " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].Speed / ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId - 1].Speed;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[1].text = "Vitesse du projectile : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].Speed;
		    }
	    }
	    if (correspondingWeaponData.hasCooldown)
	    {
		    weaponChoiceTexts.upgradesText[2].enabled = true;
		    if (correspondingWeaponData.isAnUpgrade)
		    {
			    weaponChoiceTexts.upgradesText[2].text    = "Cooldown : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId - 1].cooldownDuration + " --) " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].cooldownDuration;
			}
		    else
		    {
			    weaponChoiceTexts.upgradesText[2].text = "Cooldown : " +  ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].cooldownDuration;
		    }
	    }
	    if (correspondingWeaponData.hasPierce)
	    {
		    weaponChoiceTexts.upgradesText[3].enabled = true;
		    if (correspondingWeaponData.isAnUpgrade)
		    {
			    weaponChoiceTexts.upgradesText[3].text    = "Transpercions max. : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId - 1].pierce + " --) " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].pierce;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[3].text = "Transpercions max. : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].pierce;
		    }
	    }
	    if (correspondingWeaponData.hasKnockback)
	    {
		    weaponChoiceTexts.upgradesText[4].enabled = true;
		    if (correspondingWeaponData.isAnUpgrade)
		    {
			    weaponChoiceTexts.upgradesText[4].text    = "Force du recul * " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].knockbackForce / ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId - 1].knockbackForce;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[4].text    = "Force du recul : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].knockbackForce;
		    }
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
