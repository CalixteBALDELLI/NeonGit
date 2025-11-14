using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float           timerInSeconds;
    string                           timerText;
    [SerializeField] TextMeshProUGUI timerTextMesh;
    [SerializeField] GameObject      powerfulEnemy;
    void Start()
    {
        timerTextMesh.text = timerText;
        StartCoroutine(GameTimer());
    }
    
    IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(1);
        if (timerInSeconds > 0)
        {
            timerInSeconds--;
            timerText       = System.TimeSpan.FromSeconds(timerInSeconds).ToString("hh':'mm':'ss");
            timerTextMesh.text = timerText;
            StartCoroutine(GameTimer());
        }
        else
        {
            powerfulEnemy.SetActive(true);
        }
    }
    
    void Update()
    {
    }
}
