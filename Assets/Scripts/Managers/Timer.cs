using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float           timerInSeconds;
    string                           timerText;
    string                           finalBossTimerTexto;
    [SerializeField] TextMeshProUGUI timerTextMesh;
    [SerializeField] TextMeshProUGUI FinalBossTimerText;
    [SerializeField] GameObject      powerfulEnemyPrefab;
    [SerializeField] GameObject      finalBossPrefab;
    [SerializeField] float           timeBeforeFinalBoss;

    void Start()
    {
        SetBossTimer();
        StartCoroutine(FinalBossTimer());
    }

    void SetBossTimer()
    {
        timerInSeconds     = MapDataHolding.SINGLETON.currentmapData.timeBeforeBoss;
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
        powerfulEnemyPrefab.SetActive(true);
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
        finalBossPrefab.SetActive(true);
    }
}

