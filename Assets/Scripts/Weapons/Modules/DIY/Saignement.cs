using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Saignement : MonoBehaviour
{
    [SerializeField]         EnemyStat                enemyStat;
    [HideInInspector] public float                    speedDamageSaignement;
    [HideInInspector] public float                    damageSaignement;
    [HideInInspector] public float                    damagesSteps;
    [SerializeField]         WeaponScriptableObject[] saignementData;
    [SerializeField]         Light2D                  saignementLight;



    public void CallSaignememnt()
    {
        ModuleManager.SINGLETON.StartSaignementCooldown();
        speedDamageSaignement = saignementData[ModuleManager.SINGLETON.saignementAcquired - 1].Speed;
        damageSaignement      = saignementData[ModuleManager.SINGLETON.saignementAcquired - 1].Damage;
        Debug.Log(damageSaignement);
        if (enemyStat.isBleeding == false)
        {
            enemyStat.isBleeding = true;
            StartCoroutine(DamageEnemiesSaignement());
        }

    }

    IEnumerator DamageEnemiesSaignement()
    {
        while (true)
        {
            saignementLight.enabled = true;
            enemyStat.TakeDamage(damageSaignement);
            yield return new WaitForSeconds(speedDamageSaignement);
        }
        saignementLight.enabled = false;
    }

}
