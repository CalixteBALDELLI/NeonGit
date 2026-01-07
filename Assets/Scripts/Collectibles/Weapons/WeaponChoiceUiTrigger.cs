using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
	Canvas                                  weaponChoiceCanvas;
    public WeaponScriptableObject correspondingWeaponData;
    WeaponChoiceTexts                       weaponChoiceTexts;
    [SerializeField] int                    secondsBeforeSpawn;
    [SerializeField] BoxCollider2D          boxCollider2D;
    [SerializeField] GameObject             weaponSprite;
    [SerializeField] GameObject             questPointer;
    [SerializeField] Image                  questPointerArrow;
    [SerializeField] Image                  questPointerWeaponIcon;


    void Start()
    {
	    weaponChoiceCanvas = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
	    weaponChoiceTexts  = weaponChoiceCanvas.GetComponent<WeaponChoiceTexts>();
	    
	    weaponSprite.SetActive(false);
	    StartCoroutine(SpawnWeapon());
    }

    IEnumerator SpawnWeapon()
    {
	    yield return new WaitForSeconds(secondsBeforeSpawn);
	    boxCollider2D.enabled = true;
	    weaponSprite.SetActive(true);
	    questPointer.SetActive(true);
	    questPointerArrow.color       = ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + 1].weaponColor;
	    questPointerWeaponIcon.sprite = ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + 1].weaponIcon;
	    questPointerWeaponIcon.color  = ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + 1].weaponColor;	
    }
    
    int IndexToAdd()
    {
	    int index = 0;
	    if (correspondingWeaponData.isPropagation)
	    {
		    index = ModuleManager.SINGLETON.propagationAcquired;
	    }

	    if (correspondingWeaponData.isProjectile)
	    {
		    index = ModuleManager.SINGLETON.projectileAcquired;
	    }

	    if (correspondingWeaponData.isFoudre)
	    {
		    index = ModuleManager.SINGLETON.foudreAcquired;
	    }

	    if (correspondingWeaponData.isSaignement)
	    {
		    index = ModuleManager.SINGLETON.saignementAcquired;
	    }

	    if (correspondingWeaponData.isKnockback)
	    {
		    index = ModuleManager.SINGLETON.knockbackAcquired;
	    }
			
	    Debug.Log("Index To Add : " + index);
	    return index;
    }

    bool CheckUpgrade()
    {
	    bool isUpgrade = correspondingWeaponData.isPropagation && ModuleManager.SINGLETON.propagationAcquired >= 1
	                  || correspondingWeaponData.isProjectile  && ModuleManager.SINGLETON.projectileAcquired  >= 1
	                  || correspondingWeaponData.isFoudre      && ModuleManager.SINGLETON.foudreAcquired      >= 1
	                  || correspondingWeaponData.isKnockback   && ModuleManager.SINGLETON.knockbackAcquired   >= 1
	                  || correspondingWeaponData.isSaignement  && ModuleManager.SINGLETON.saignementAcquired  >= 1
	                  || correspondingWeaponData.isRuban       && ModuleManager.SINGLETON.rubanAcquired       >= 1
	                  || correspondingWeaponData.isRebond      && ModuleManager.SINGLETON.rebondAcquired      >= 1;
		    
	    return isUpgrade;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
		//Debug.Log("Weapon Choice Trigger");
	    Time.timeScale                               = 0;

	    foreach (var text in weaponChoiceTexts.upgradesText)
	    {
		    text.text = null;
		    text.enabled = false;
	    }
	    
	    weaponChoiceTexts.weaponNameText.text  = correspondingWeaponData.weaponName;
	    weaponChoiceTexts.descriptionText.text = correspondingWeaponData.description;
	    
	    if (correspondingWeaponData.dealDamages)
	    {
		    weaponChoiceTexts.upgradesText[0].enabled = true;
		    if (CheckUpgrade())
		    {
			    weaponChoiceTexts.upgradesText[0].text = "Dégâts : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd()].Damage + " --) " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd() + 1].Damage;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[0].text = "Dégâts : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].Damage;
		    }
	    }
	    if (correspondingWeaponData.hasSpeed)
	    {
		    weaponChoiceTexts.upgradesText[1].enabled = true;
		    if (CheckUpgrade())
		    {
			    Debug.Log("Is Upgrade");
			    weaponChoiceTexts.upgradesText[1].text    = "Vitesse du projectile * " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd() + 1].Speed / ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].Speed;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[1].text = "Vitesse du projectile : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].Speed;
		    }
	    }
	    if (correspondingWeaponData.hasCooldown)
	    {
		    weaponChoiceTexts.upgradesText[2].enabled = true;
		    if (CheckUpgrade())
		    {
			    weaponChoiceTexts.upgradesText[2].text    = "Cooldown : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd()].cooldownDuration + " --) " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd() + 1].cooldownDuration;
			}
		    else
		    {
			    weaponChoiceTexts.upgradesText[2].text = "Cooldown : " +  ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].cooldownDuration;
		    }
	    }
	    if (correspondingWeaponData.hasPierce)
	    {
		    weaponChoiceTexts.upgradesText[3].enabled = true;
		    if (CheckUpgrade())
		    {
			    weaponChoiceTexts.upgradesText[3].text    = "Transpercions max. : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd()].pierce + " --) " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd() + 1].pierce;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[3].text = "Transpercions max. : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].pierce;
		    }
	    }
	    if (correspondingWeaponData.hasKnockback)
	    {
		    weaponChoiceTexts.upgradesText[4].enabled = true;
		    if (CheckUpgrade())
		    {
			    weaponChoiceTexts.upgradesText[4].text    = "Force du recul * " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd() + 1].knockbackForce / ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId + IndexToAdd() + 1].knockbackForce;
		    }
		    else
		    {
			    weaponChoiceTexts.upgradesText[4].text    = "Force du recul : " + ModuleManager.SINGLETON.modulesData[correspondingWeaponData.weaponId].knockbackForce;
		    }
	    }

	    //weaponChoiceTexts.upgradeText.text     =;
	    //weaponChoiceTexts.damagesAndCooldownText.text = "Dégâts : " + weaponData.Damage             + " Cooldown : " + weaponData.cooldownDuration;
	    weaponChoiceTexts.xpGainText.text         = "+ " + correspondingWeaponData.weaponId + IndexToAdd() + " XP";
	    weaponChoiceCanvas.enabled                = true;
	    ModuleManager.SINGLETON.weaponToEquip     = correspondingWeaponData.weaponId;
	    PlayerStats.SINGLETON.xpToExchange        = correspondingWeaponData.weaponId + IndexToAdd();
	    ModuleManager.SINGLETON.pickedWeapon      = gameObject;
	    ModuleManager.SINGLETON.pickedWeaponArrow = questPointer;
	    weaponChoiceTexts.weaponIcon.sprite       = correspondingWeaponData.weaponIcon;
    }
}
