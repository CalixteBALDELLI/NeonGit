using UnityEngine;

public class InGamePlayerStatUpgrade : MonoBehaviour
{
    PlayerStats playerStats;
    public enum InGamePlayerStatUpgrades
    {
        speed,
        Damages,
        Item3,
    }
    
    [SerializeField]
    InGamePlayerStatUpgrades inGamePlayerStatUpgrades;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
