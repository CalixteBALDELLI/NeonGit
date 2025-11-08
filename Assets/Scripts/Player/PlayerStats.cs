using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;
    
    //Stats actuelles
    //[HideInInspector] 
    public float currentHealth;
    float        currentRecovery;
    float        currentMoveSpeed;
    float        currentMight;
    float        currentProjectileSpeed;
    float        currentDamages;
    public float speedToAdd;
    public float damagesToAdd;
    public float critChancesToAdd;
    public float swordDistanceToAdd;
    public float  swordRadiusToAdd;

    //Experience and level of the player
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;
    
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
        StatsReset();
    }
    void StatsReset()
    {
        currentHealth          = characterData.MaxHealth;
        currentRecovery        = characterData.Recovery;
        currentMoveSpeed       = characterData.MovingSpeed;
        currentMight           = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;

        currentDamages += damagesToAdd;
        currentMoveSpeed += speedToAdd;
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
            level++;
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
        }
    }
    
    // In Game Upgrades
    void DamageUpgrade()
    {
        currentDamages++;
    }

    void SpeedUpgrade()
    {
        currentMoveSpeed++;
    }

    void SwordLengthUpgrade()
    {
        
    }

    void SwordRadiusUpgrade()
    {
        
    }

    void CriticalChanceUpgrade()
    {
        
    }

    // Out Game Upgrades
    void OutGameDamagesUpgrade()
    {
        damagesToAdd++;
    }
    
    
}

