using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModuleManager : MonoBehaviour
{
    public static    ModuleManager        SINGLETON;
    [SerializeField] ProjectileController projectileController;
    [SerializeField] GameObject           projectileControllerGameObject;

    [SerializeField] public int propagationAcquired;
    [SerializeField] public int knockbackAcquired;
    [SerializeField] public int projectileAcquired;

    [SerializeField] public WeaponScriptableObject projectileLvl2;
    [SerializeField] public WeaponScriptableObject projectileLvl3;

    [SerializeField] public WeaponScriptableObject knockbackLvl2;
    [SerializeField] public WeaponScriptableObject knockbackLvl3;

    [SerializeField] ProjectileController weaponController;
    [SerializeField] Canvas               inventoryFullMessage;

    public bool propagationInProgress;
    public int  currentPropagationStep;

    [SerializeField] Image[] inventoryIcons;
    [SerializeField] Image[] inventoryBackgrounds;
    int                      weaponIconIndex;
    public Sprite[]          modulesIcons;
    public List<int>         currentEquipedModules = new List<int>();


    [HideInInspector] public int        weaponToEquip;
    [HideInInspector] public Sprite     weaponToEquipSprite;
    [HideInInspector] public GameObject pickedWeapon;
    public                   int        equippedWeapons;
    Canvas                              weaponChoiceCanvas;

    [Header("Damage Text Settings")]
    public Canvas damageTextCanvas;

    public        float         textFontSize = 20;
    public        TMP_Asset     textFont;
    public        Camera        referenceCamera;
    public static ModuleManager instance;

    public float propagationCooldown;

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
        Debug.LogWarning("Module Equiped");
        instance = this;

        if (equippedWeapons == 6)
        {
            inventoryFullMessage.enabled = true;
        }
        else
        {
            weaponChoiceCanvas = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
            if (weaponToEquip == 0 && projectileAcquired == 0) // Equipement du Projectile
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

            if (weaponToEquip == 1) // Equipement du Knockback
            {
                knockbackAcquired++;
            }

            if (weaponToEquip == 2) // Equipement de la Propagation
            {
                propagationAcquired++;
            }

            equippedWeapons++;
            weaponChoiceCanvas.enabled = false;
            Time.timeScale             = 1;
            InventoryUiUpdate();
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
        Debug.LogWarning("Inventory UI Update");
        currentEquipedModules.Add(weaponToEquip);
        Debug.Log("Added Module : " + weaponToEquip);
        currentEquipedModules.Sort();
        //int equippedModuleIconIndex = 0;
        int inventoryIndex = 0;
        foreach (int equippedModule in currentEquipedModules)
        {
            Debug.Log("Inventory Index : " + inventoryIndex);
            Debug.Log("Eqquiped Module : " + equippedModule);
            inventoryIcons[inventoryIndex].enabled = true;
            inventoryIcons[inventoryIndex].sprite  = modulesIcons[equippedModule];
            inventoryIndex++;
        }
        //inventoryIcons[weaponIconIndex].sprite  = weaponToEquipSprite;
        //inventoryIcons[weaponIconIndex].enabled = true;
        //weaponIconIndex++;
        //Debug.Log(weaponIconIndex);
    }

    public IEnumerator PropagationCooldown()
    {
        Debug.LogWarning("Propagation Cooldown");
        propagationCooldown = 2;
        float delayBetweenDecrease = 0.25f;
        while (propagationCooldown > 0)
        {
            propagationCooldown -= 0.1f ;
            Debug.Log("Propagation Cooldown : " + propagationCooldown);
            yield return new WaitForSeconds(delayBetweenDecrease);
        }
        Debug.LogWarning("Propagation Cooldown Finished");
        propagationCooldown = 0;
    }
}
