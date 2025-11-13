using TMPro;
using UnityEngine;

public class MonyManager : MonoBehaviour
{
    public static MonyManager     instance;
    public        TextMeshProUGUI MoneyText;
    public        int             Money;

    void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        RefreshText();
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int scoreToAdd)
    {
        Money += scoreToAdd;
        RefreshText();
    }
    
    void RefreshText()
    {
        MoneyText.text = " " +  Money;
    }
   
}