using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public CharacterScriptableObject characterData;
    
    //Stats actuelles
    //[HideInInspector] 
    public float                            currentPlayerDamage;
    public float                            currentHealth;
    public float                            maxHealth; 
    float                                   currentAutoHealthRegeneration;
    public float                            currentMoveSpeed;
    float                                   currentProjectileSpeed;
    public           float                  currentSwordDamages;
    public           float                  currentModulesDamages;
    public           float                  currentSwordSwingSpeed;
    public           float                  currentSwordCooldown;
    public           float                  currentSwordAndModulesUpgrade;
    public           float                  currentXpGain;
    public           int                    currentMoney;
    public           float                  speedToAdd;
    public           float                  critChancesToAdd;
    public           float                  swordDistanceToAdd;
    public           float                  swordRadiusToAdd;
    public           float                  swordAndModulesUpgradeToAdd;
    public           int                    xpToExchange;
    public           bool                   teleporterKeyObtained;
    [SerializeField] SwordManager           swordManager;
    [SerializeField] GameObject             swordChildren;
    [SerializeField] Canvas                 upgradesMenu;
    [SerializeField] InGameUpgrades         inGameUpgrades;
    [SerializeField] OutGameUpgrades        outGameUpgrades;
    [SerializeField] OutGameUpgradesCosts   outGameUpgradesCosts;
    [SerializeField] WeaponScriptableObject swordData;
    HUDUpdate              hudUpdater;


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
        if (instance == null)
        {
            instance = this;
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
        maxHealth                     = characterData.MaxHealth;
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
            Debug.Log("Level up");
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
    public void UpgradeSwordAndModulesDamage()
    {
        currentSwordDamages += inGameUpgrades.swordAndModulesUpgrade;
        currentSwordAndModulesUpgrade += inGameUpgrades.swordAndModulesUpgrade;
    }

    public void UpgradeSwordDamage()
    {
        currentSwordDamages += inGameUpgrades.swordDamages;
    }
    public void UpgradeSpeed()
    {
        currentMoveSpeed += inGameUpgrades.playerSpeed;
    }

    public void UpgradeSwordLength()
    {
        swordChildren.transform.localScale += new Vector3(inGameUpgrades.swordLength,0,0);
    }

    public void UpgradeSwordRadius()
    {
        swordManager.swingCardinalRadius += inGameUpgrades.swordRadius;
    }

    public void UpgradeSwordSpeed()
    {
        currentSwordSwingSpeed += inGameUpgrades.swordSpeed;
    }

    public void UpgradeSwordCooldown()
    {
        currentSwordCooldown -= inGameUpgrades.swordCooldownToDecrease;
    }

    public void UpgradeCriticalHitChance()
    {
        
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
            maxHealth += outGameUpgrades.maxHealth;
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


