using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModuleManager : MonoBehaviour
{
    public static    ModuleManager     SINGLETON;
    [SerializeField] GameObject        projectileControllerGameObject;
    [SerializeField] ProjectileSpawner projectileSpawner;
    [SerializeField] GameObject        rubanGameObject;
    [SerializeField] ScieCirculaire    rebondScript;
    
    [Header("Player Inventory")]
    [SerializeField] public int propagationAcquired;
    [SerializeField] public int knockbackAcquired;
    [SerializeField] public int projectileAcquired;
    [SerializeField] public int foudreAcquired;
    [SerializeField] public int saignementAcquired;
    [SerializeField] public int rubanAcquired;
    [SerializeField] public int rebondAcquired;
    [Header("Modules Data & Scripts")]
    public WeaponScriptableObject[] modulesData;
    public FoudreScript foudreScript;
    
    [Header("Damage Text Settings")]
    [SerializeField] Canvas               inventoryFullMessage;
    
    [Header("InGame Propagation State")]
    public int  currentPropagationStep;
    public bool propagationCooldownFinished = true;
    public bool projectileCooldownFinished  = true;
    public bool knockbackCooldownFinished   = true;
    public bool saignementCooldownFinished  = true;


    [Header("Inventory UI")]
    [SerializeField] Image[] inventoryIcons;
    [SerializeField] Image[] inventoryBackgrounds;
    int                      weaponIconIndex;
    public Sprite[]          modulesIcons;
    public List<int>         currentEquipedModules = new List<int>();
    
    [SerializeField] Slider propagationSlider;
    [SerializeField] Slider projectileSlider;
    [SerializeField] Slider saignementSlider;
    [SerializeField] Slider knockbackSlider;
    [SerializeField] Slider foudreSlider;


    [Header("InGame Inventory Management")]
     public int        weaponToEquip;
    [HideInInspector] public GameObject pickedWeapon;
    [HideInInspector] public GameObject pickedWeaponArrow;
    public                   int        equippedWeapons;
    Canvas                              weaponChoiceCanvas;

    [Header("Damage Text Settings")]
    public Canvas damageTextCanvas;
    public        float         textFontSize = 20;
    public        TMP_Asset     textFont;
    public        Camera        referenceCamera;
    public static ModuleManager instance;


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
        //Debug.LogWarning("Module Equipped");
        instance = this;

        if (equippedWeapons == 6)
        {
            inventoryFullMessage.enabled = true;
        }
        else
        {
            weaponChoiceCanvas = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
            if (weaponToEquip == 0) // Equipement du Projectile
            {
                projectileAcquired++;
                projectileControllerGameObject.SetActive(true);
            }

            if (weaponToEquip == 4) // Equipement du Knockback
            {
                knockbackAcquired++;
            }

            if (weaponToEquip == 8) // Equipement de la Propagation
            {
                propagationAcquired++;
            }

            if (weaponToEquip == 16) // Equipement de la Foudre
            {
                foudreAcquired++;
                foudreScript.HitZone();
            }

            if (weaponToEquip == 11) // Equipement du Saignement
            {
                saignementAcquired++;
            }

            if (weaponToEquip == 24)
            {
                Debug.Log("Rebond Equipped");
                rebondAcquired++;
                StopCoroutine(rebondScript.CreateScie());
                StartCoroutine(rebondScript.CreateScie());
            }

            if (weaponToEquip == 20)
            {
                rubanAcquired++;
                rubanGameObject.SetActive(true);
            }

            equippedWeapons++;
            weaponChoiceCanvas.enabled = false;
            Time.timeScale             = 1;
            InventoryUiUpdate();
            Destroy(pickedWeaponArrow);
            Destroy(pickedWeapon);
        }
    }

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (!instance.damageTextCanvas) return;

        if (!instance.referenceCamera) instance.referenceCamera = Camera.main;

        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(text, target, duration, speed));
    }



    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f)
    {
        GameObject      textObj = new GameObject("Damage Floating Text");
        RectTransform   rect    = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmPro   = textObj.AddComponent<TextMeshProUGUI>();
        tmPro.text                = "-" + text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.fontSize            = textFontSize;
        if (textFont) tmPro.font = (TMP_FontAsset) textFont;
        rect.position = referenceCamera.WorldToScreenPoint(target.position);

        Destroy(textObj, duration);

        textObj.transform.SetParent(instance.damageTextCanvas.transform);

        WaitForEndOfFrame w       = new WaitForEndOfFrame();
        float             t       = 0;
        float             yOffset = 0;
        while (t < duration)
        {


            yield return w;
            t += Time.deltaTime;

            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1 - t / duration);

            yOffset       += speed * Time.deltaTime;
            rect.position =  referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, yOffset, 0));
        }

    }

    void InventoryUiUpdate()
    {
        //Debug.LogWarning("Inventory UI Update");
        currentEquipedModules.Add(weaponToEquip);
        //Debug.Log("Added Module : " + weaponToEquip);
        currentEquipedModules.Sort();
        //int equippedModuleIconIndex = 0;
        int inventoryIndex = 0;
        foreach (int equippedModule in currentEquipedModules)
        {
            //Debug.Log("Inventory Index : " + inventoryIndex);
            //Debug.Log("Eqquiped Module : " + equippedModule);
//            inventoryIcons[inventoryIndex].enabled = true;
            //inventoryIcons[inventoryIndex].sprite  = modulesIcons[equippedModule];
            inventoryIndex++;
        }
    }

    public void StartPropagationCooldown()
    {
        StartCoroutine(PropagationCooldown(modulesData[9 + propagationAcquired].CooldownDuration));
    }

    public void StartKnockbackCooldown()
    {
        StartCoroutine(KnockbackCooldown(modulesData[4 + knockbackAcquired].CooldownDuration));
    }

    public void StartSaignementCooldown()
    {
        StartCoroutine(SaignementCooldown(modulesData[11 + saignementAcquired].CooldownDuration));
    }
    
    public IEnumerator PropagationCooldown(float startCooldown)
    {
        //Debug.LogWarning("Propagation Cooldown");
        propagationCooldownFinished = false;
        float cooldown = startCooldown;
        //float propagationCooldown  = 3;
        float delayBetweenDecrease = 1f;
        float cooldownReduction = 1f;
        propagationSlider.maxValue = startCooldown;
        do
        {
            cooldown -= cooldownReduction;
            Debug.Log("Propagation Cooldown : " + cooldown);
            propagationSlider.value = cooldown;
            yield return new WaitForSeconds(delayBetweenDecrease);
        } while (cooldown >= cooldownReduction);
        Debug.LogWarning("Propagation Cooldown Finished");
        //propagationCooldown = 0;
        propagationCooldownFinished = true;
        propagationSlider.value           = propagationSlider.maxValue;
    }

    public IEnumerator ProjectileCooldown(float startCooldown)
    {
        Debug.LogWarning("Module Cooldown");
        float cooldown = startCooldown;
        projectileCooldownFinished = false;
        projectileSlider.maxValue = startCooldown;
        //float cooldown  = 3;
        float delayBetweenDecrease = 1f;
        float cooldownReduction    = 1f;
        do
        {
            cooldown -= cooldownReduction;
            Debug.Log("Module Cooldown : " + cooldown);
            projectileSlider.value = cooldown;
            yield return new WaitForSeconds(delayBetweenDecrease);
        } while (cooldown >= cooldownReduction);

        projectileSlider.value = projectileSlider.maxValue;
        projectileSpawner.SpawnProjectile();
    }
    
    public IEnumerator KnockbackCooldown(float startCooldown)
    {
        Debug.LogWarning("Knockback Cooldown");
        float cooldown = startCooldown;
        knockbackCooldownFinished = false;
        knockbackSlider.maxValue  = startCooldown;
        //float cooldown  = 3;
        float delayBetweenDecrease = 1f;
        float cooldownReduction    = 1f;
        do
        {
            cooldown -= cooldownReduction;
            Debug.Log("Knockback Cooldown : " + cooldown);
            knockbackSlider.value = cooldown;
            yield return new WaitForSeconds(delayBetweenDecrease);
        } while (cooldown >= cooldownReduction);
        
        knockbackSlider.value     = knockbackSlider.maxValue;
        knockbackCooldownFinished = true;
    }
    
    public IEnumerator FoudreCooldown(float startCooldown)
    {
        Debug.LogWarning("Foudre Cooldown");
        float cooldown = startCooldown;
        foudreSlider.maxValue = startCooldown;
        //float cooldown  = 3;
        float delayBetweenDecrease = 1f;
        float cooldownReduction    = 1f;
        do
        {
            cooldown -= cooldownReduction;
            Debug.Log("Foudre Cooldown : " + cooldown);
            foudreSlider.value = cooldown;
            yield return new WaitForSeconds(delayBetweenDecrease);
        } while (cooldown >= cooldownReduction);
        
        foudreSlider.value = foudreSlider.maxValue;
        foudreScript.HitZone();
    }
    
    public IEnumerator SaignementCooldown(float startCooldown)
    {
        Debug.LogWarning("Saignement Cooldown");
        float cooldown = startCooldown;
        saignementCooldownFinished = false;
        saignementSlider.maxValue = startCooldown;
        //float cooldown  = 3;
        float delayBetweenDecrease = 1f;
        float cooldownReduction    = 1f;
        do
        {
            cooldown -= cooldownReduction;
            Debug.Log("Saignement Cooldown : " + cooldown);
            saignementSlider.value = cooldown;
            yield return new WaitForSeconds(delayBetweenDecrease);
        } while (cooldown >= cooldownReduction);
        
        saignementSlider.value     = saignementSlider.maxValue;
        saignementCooldownFinished = true;
    }
}
