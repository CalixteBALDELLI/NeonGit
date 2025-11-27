using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    Canvas                                  weaponChoiceCanvas;
    [SerializeField] WeaponCollectibleData  weaponCollectibleData;
    WeaponChoiceTexts                       weaponChoiceTexts;

    private void OnTriggerEnter2D(Collider2D other)
    {
	    Debug.Log("Weapon Choice Trigger");
	    weaponChoiceCanvas                           = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
	    weaponChoiceTexts                            = weaponChoiceCanvas.GetComponent<WeaponChoiceTexts>();
	    Time.timeScale                               = 0;

	    weaponChoiceTexts.weaponNameText.text  = weaponCollectibleData.weaponName;
	    weaponChoiceTexts.descriptionText.text = weaponCollectibleData.description;
	    //weaponChoiceTexts.upgradeText.text     = weaponCollectibleData;
	    //weaponChoiceTexts.damagesAndCooldownText.text = "Dégâts : " + weaponData.Damage             + " Cooldown : " + weaponData.cooldownDuration;
	    weaponChoiceTexts.xpGainText.text           = "+ " + weaponCollectibleData.xpValue + " XP";
	    weaponChoiceCanvas.enabled                  = true;
	    ModuleManager.SINGLETON.weaponToEquip       = weaponCollectibleData.weaponId;
	    ModuleManager.SINGLETON.weaponToEquipSprite = weaponCollectibleData.weaponIcon;
	    PlayerStats.SINGLETON.xpToExchange          = weaponCollectibleData.xpValue;
	    ModuleManager.SINGLETON.pickedWeapon        = gameObject;
    }
}
