using TMPro;
using UnityEngine;

public class HUDUpdate : MonoBehaviour
{
    public        TextMeshProUGUI moneyText;
    [SerializeField] PlayerStats playerStats;
    
    public void RefreshText()
    {
        moneyText.text = " " +  playerStats.currentMoney;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
