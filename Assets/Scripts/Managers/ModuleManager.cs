using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    
    [SerializeField] KnifeController weaponController;
    [SerializeField] Canvas          inventoryFullMessage;
    [SerializeField] public int           BACKUPTIMER = 30;

    public bool propagationInProgress;
    public int  currentPropagationStep;

    [Header("Damage Text Settings")]
    public Canvas damageTextCanvas;
    public float         textFontSize = 20;
    public TMP_FontAsset textFont;
    public Camera        referenceCamera;
    public static ModuleManager instance;
    
    [HideInInspector]
    public int          weaponToEquip;
    [HideInInspector]
    public GameObject   pickedWeapon;
    [HideInInspector]
    public int          equippedWeapons;
    Canvas              weaponChoiceCanvas;

    public void WeaponEquiping()
    {
        if (equippedWeapons == 6)
        {
            inventoryFullMessage.enabled = true;        
        }
        else
        {
            weaponChoiceCanvas = GameObject.Find("Weapon Choice").GetComponent<Canvas>();
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
            Time.timeScale             = 1;
            Destroy(pickedWeapon);
        }
    }

    public IEnumerator BackupTimer()
    {
        
        if (propagationInProgress && BACKUPTIMER == 30)
        {
            propagationInProgress = false;
            BACKUPTIMER           = 0;
        }
        else
        {
            yield return new WaitForSeconds(1);
            BACKUPTIMER++;
            StartCoroutine(BackupTimer());
        }
    }

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        
        if (!instance.damageTextCanvas) return;
        
        if (!instance.referenceCamera) instance.referenceCamera = Camera.main;
        
        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(
             text, target, duration, speed 
        ));
    }

    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f)
    {
        GameObject    textObj =new GameObject("Damage Floating Text");
        RectTransform rect    = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI   tmPro   = textObj.AddComponent<TextMeshProUGUI>();
        tmPro.text = "-"+text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.fontSize = textFontSize;
        if(textFont) tmPro.font = textFont;
        rect.position = referenceCamera.WorldToScreenPoint(target.position);
        
        Destroy(textObj,duration);
        
        textObj.transform.SetParent(instance.damageTextCanvas.transform);
        
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;
        float yOffset = 0;
        while(t < duration)
        {
            
            
            yield return w;
            t+=Time.deltaTime;

            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1-t/ duration);
            
            yOffset += speed * Time.deltaTime;
            rect.position = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, yOffset, 0));
            
            
        }

    }
    void Awake()
    {
        instance = this;
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


