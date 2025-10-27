using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdata : MonoBehaviour
{
    public TextMeshProUGUI text;
    public HealthManager   gameManager;

    void Update()
    {
        //text = gameManager.playerHealth.ToString();
    }
}
