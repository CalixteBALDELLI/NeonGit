using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackModule : MonoBehaviour
{
    public           Rigidbody2D              rb;
    public           float                    currentKnockbackForce;
    public           WeaponScriptableObject[] knockbackData;
    public           int                      currentKnockbackStep;
    public           int                      maxKnockbackSteps;
    public           EnemyStat                enemyStat;
    [SerializeField] EnemyMouvement           enemyMouvement;
    
    
    public void KnockbackSetup()
    {
        //maxKnockbackSteps = knockbackData[ModuleManager.SINGLETON.knockbackAcquired - 1].maxKnockBackSteps;
        if (currentKnockbackStep < knockbackData[ModuleManager.SINGLETON.knockbackAcquired - 1].maxKnockBackSteps)
        {
            //Debug.Log(enemyStat.spawnPosition + "Max Knockback Steps :  " + maxKnockbackSteps);
            if (ModuleManager.SINGLETON.knockbackAcquired == 1)
            {
                currentKnockbackForce = knockbackData[ModuleManager.SINGLETON.knockbackAcquired - 1].knockbackForce;
                if (currentKnockbackStep > 0)
                {
                    currentKnockbackForce /= currentKnockbackStep;
                    //Debug.Log(enemyStat.spawnPosition + " Knockback Force : " + currentKnockbackForce);
                }

                StartCoroutine(KnockbackStart());
            }
            else if (ModuleManager.SINGLETON.knockbackAcquired == 2)
            {
                //Debug.Log("Knockback 2");
                currentKnockbackForce = knockbackData[1].knockbackForce;
                StartCoroutine(KnockbackStart());
            }
            else if (ModuleManager.SINGLETON.knockbackAcquired == 3)
            {
                //Debug.Log("Knockback 3");
                currentKnockbackForce = knockbackData[2].knockbackForce;
                StartCoroutine(KnockbackStart());
            }
        }
        else
        {
            //Debug.Log(enemyStat.spawnPosition + " Max Knockbacks Reached");
            currentKnockbackStep = 0;
        }
    }

    IEnumerator KnockbackStart()
        {
            if (ModuleManager.SINGLETON.knockbackAcquired > 0)
            {
                //Debug.Log(gameObject.name + " Knockback");
                enemyMouvement.isKnockedBack = true;
                yield return new WaitForSeconds(0.5f);
                enemyMouvement.isKnockedBack        = false;
                currentKnockbackStep = 0;
                //Debug.Log(enemyStat.spawnPosition + " Knockback Movement Finished");
            }
        }
    
    public void KnockbackMovement()
    {
        Vector2 knockbackDirection = (rb.position - (Vector2) PlayerStats.SINGLETON.playerTransform.transform.position).normalized;
        rb.linearVelocity = knockbackDirection * currentKnockbackForce;
    }
}
