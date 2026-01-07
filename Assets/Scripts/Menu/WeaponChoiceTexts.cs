using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponChoiceTexts : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI   newModuleText;
    [SerializeField] public TextMeshProUGUI   weaponNameText;
    [SerializeField] public TextMeshProUGUI   descriptionText;
    [SerializeField] public TextMeshProUGUI[] upgradesText;
    [SerializeField] public TextMeshProUGUI   xpGainText;
    [SerializeField] public TextMeshProUGUI   subDescriptionText;
    [SerializeField] public Image weaponIcon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
