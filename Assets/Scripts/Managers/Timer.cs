using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float           timerInSeconds;
    string                           timerText;
    string                           finalBossTimerTexto;
    [SerializeField] TextMeshProUGUI timerTextMesh;
    [SerializeField] TextMeshProUGUI FinalBossTimerText;
    [SerializeField] GameObject      powerfulEnemyPrefab;
    [SerializeField] GameObject      finalBossPrefab;
    [SerializeField] float           timeBeforeFinalBoss;
    public static Timer SINGLETON;

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
    }
    
    void Start()
    {
        SetBossTimer(75);
        StartCoroutine(FinalBossTimer());
    }

    public void SetBossTimer(float time)
    {
        timerInSeconds = time;
        //timerInSeconds     = MapDataHolding.SINGLETON.currentmapData.timeBeforeBoss;
        timerTextMesh.text = timerText;
        StartCoroutine(BossTimer());
    }

    IEnumerator BossTimer()
    {
        while (timerInSeconds > 0)
        {
            timerInSeconds--;
            timerText          = "Boss de Zone dans : " + System.TimeSpan.FromSeconds(timerInSeconds).ToString("hh':'mm':'ss");
            timerTextMesh.text = timerText;
            yield return new WaitForSeconds(1);
        }

        Debug.Log("Boss Timer finished");
        Instantiate(powerfulEnemyPrefab);
        //powerfulEnemyPrefab.SetActive(true);
    }

    IEnumerator FinalBossTimer()
    {

        while (timeBeforeFinalBoss > 0)
        {
            timeBeforeFinalBoss--;
            finalBossTimerTexto     = "Boss Final dans : " + System.TimeSpan.FromSeconds(timeBeforeFinalBoss).ToString("hh':'mm':'ss");
            FinalBossTimerText.text = finalBossTimerTexto;
            yield return new WaitForSeconds(1);
        }
        Instantiate(finalBossPrefab);
    }
}

