using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats               SINGLETON;
    public        Transform                 playerTransform;
    public        CharacterScriptableObject characterData;
    public        bool                      hasLeveledUp;
    
    //Stats actuelles
    //[HideInInspector]
    [Header("Current Stats")]
    public float currentPlayerDamage;
    public                  float  currentHealth;
    [HideInInspector] public float  currentmaxHealth; 
    float                          currentAutoHealthRegeneration;
    public                   float currentMoveSpeed;
    [HideInInspector]        float currentProjectileSpeed;
    [HideInInspector] public float currentSwordDamages;
    [HideInInspector] public float currentModulesDamages;
    [HideInInspector] public float currentSwordSwingSpeed;
    [HideInInspector] public float currentSwordCooldown;
    [HideInInspector] public float currentSwordAndModulesUpgrade;
    [HideInInspector] public float currentXpGain;
    [HideInInspector] public int   currentMoney;
    public                   int   xpToExchange;
    
    //[Header("Current Out Game Upgrades")]
    [HideInInspector]public           float                  speedToAdd;
    [HideInInspector] public float critChancesToAdd;
    [HideInInspector] public float swordDistanceToAdd;
    [HideInInspector] public float swordRadiusToAdd;
    [HideInInspector] public float swordAndModulesUpgradeToAdd;
    
    public                   bool  teleporterKeyObtained;

    [Header("Player GameObjects")]
    [SerializeField] SwordManager           swordManager;
    [SerializeField] GameObject             swordChildren;
    [Header("Upgrades Data + Upgrades Menu")]
    [SerializeField] InGameUpgrades         inGameUpgrades;
    [SerializeField] OutGameUpgrades        outGameUpgrades;
    [SerializeField] OutGameUpgradesCosts   outGameUpgradesCosts;
    [SerializeField] WeaponScriptableObject swordData;
    [SerializeField] Canvas                 upgradesMenu;


    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int             level = 1;
    public int             experienceCap;
    public Image           healthBar;
    public TextMeshProUGUI xpText;
    public Image           xpBar;
    public TextMeshProUGUI MoneyText;
    
    
    
    //Class pour définir un objectif de niveau et la correspondance du cap d'xp qui augmente pour cet objectif
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    
    public List<LevelRange> levelRanges;

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
        StatsReset();
    }
    
    void StatsReset()
    {
        // Setup des variables au début du jeu selon les données par défaut
        currentHealth                 = characterData.MaxHealth;
        currentmaxHealth              = characterData.MaxHealth;
        currentAutoHealthRegeneration = characterData.AutoHealthRegeneration;
        currentMoveSpeed              = characterData.MovingSpeed;
        currentProjectileSpeed        = characterData.ProjectileSpeed;
        currentSwordSwingSpeed        = swordData.Speed;
        currentSwordDamages           = swordData.Damage;
        currentPlayerDamage           = characterData.damages;
        //application des améliorations faites Out Game
        currentMoveSpeed      += speedToAdd;
        currentSwordDamages   += swordAndModulesUpgradeToAdd;
    }
    void Start()
    {
        //Initialise le cap d'xp au premier cap d'xp d'augmentation de niveau
        experienceCap = levelRanges[0].experienceCapIncrease;
        
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            hasLeveledUp = true;
            level++;
            upgradesMenu.enabled = true;
            Time.timeScale       = 0;
            experience -= experienceCap;

            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
            
            UpdateLevelText();
            
        }
    }
    public void ModuleExchange()
    {
        IncreaseExperience(xpToExchange);
    }
    

    void Update()
    {
        xpBar.fillAmount     = (float)experience / experienceCap;
        healthBar.fillAmount = currentHealth     / characterData.MaxHealth;
        MoneyText.text       = " " +  currentMoney;
        RefreshText();
    }
    
    void UpdateLevelText()
    {
        xpText.text = "LV: " + level.ToString();
    }

    
    public void AddMoney(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
    }
    
    void RefreshText()
    {
        MoneyText.text = " " +  currentMoney;
    }
    
    // In Game Upgrades
    
    public void UpgradePlayerDamages()
    {
        currentPlayerDamage += inGameUpgrades.playerDamages;
        hasLeveledUp        =  false;
    }
    public void UpgradePlayerMoveSpeed()
    {
        currentMoveSpeed += inGameUpgrades.playerSpeed;
        hasLeveledUp     =  false;
    }

    // améliorations Epée
    public void UpgradeSwordLength()
    {
        swordChildren.transform.localScale += new Vector3(inGameUpgrades.swordLength,0,0);
        hasLeveledUp                       =  false;
    }

    public void UpgradeSwordRadius()
    {
        swordManager.swingCardinalRadius += inGameUpgrades.swordRadius;
        hasLeveledUp                     =  false;
    }

    public void UpgradeSwordSpeed()
    {
        currentSwordSwingSpeed += inGameUpgrades.swordSpeed;
        hasLeveledUp           =  false;
    }

    public void UpgradeSwordCooldown()
    {
        currentSwordCooldown -= inGameUpgrades.swordCooldownToDecrease;
        hasLeveledUp         =  false;
    }

    
    // Out Game Upgrades
    public void OutGameUpgradeDamages()
    {
        float cost = outGameUpgradesCosts.swordAndModulesUpgrade;
        if (cost < currentMoney)
        {
            currentSwordAndModulesUpgrade += outGameUpgrades.swordAndModulesUpgrade;
        }
        else
        {
            Debug.Log("Pas assez de sous");
        }
    }

    public void OutGameUpgradeSpeed()
    {
        float cost = outGameUpgradesCosts.playerSpeed;
        if (cost < currentMoney)
        { 
            speedToAdd += outGameUpgrades.playerSpeed;
        }
        else
        {
            Debug.Log("Pas assez de sous");
        }
    }

    public void OutGameUpgradeMaxHealth()
    {
        float cost = outGameUpgradesCosts.maxHealth;
        if (cost < currentMoney)
        {
            currentmaxHealth += outGameUpgrades.maxHealth;
        }
        else
        {
            Debug.Log("Pas assez de sous");
        }
    }

    public void OutGameUpgradeXpGain()
    {
        float cost = outGameUpgradesCosts.xpGain;
        if (cost < currentMoney)
        {
             currentXpGain += outGameUpgrades.xpGain;
        }
        else
        {
            Debug.Log("Pas assez de sous");
        }
    }

    public void OutGameUpgradeAutoHealthRegeneration()
    {
        float cost = outGameUpgradesCosts.maxHealth;
        if (cost < currentMoney)
        {
            currentAutoHealthRegeneration += outGameUpgrades.autoHealthRegeneration;
        }
        else
        {
            Debug.Log("Pas assez de sous");
        }
    }
}


