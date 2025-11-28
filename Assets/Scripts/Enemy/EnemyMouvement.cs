using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    public           EnemyScriptableObject    enemyData;
    private          Transform                player;
    public           bool                     isKnockedBack;
    public           EnemyStat                enemyStat;
    public           float                    currentKnockbackForce;
    public           Rigidbody2D              rb;
    public           bool                     isStunned;
    [SerializeField] bool                     DONOTMOVE;
    public           WeaponScriptableObject[] knockbackData;
    public           int                      currentKnockbackStep;
    public           int                      maxKnockbackSteps;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isKnockedBack == false && isStunned == false && DONOTMOVE == false)
        { 
            rb.linearVelocity = Vector2.zero;
            transform.position               = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime);
        }
        else if (isKnockedBack && enemyStat.isElectrocuted == false)
        {
            KnockbackMovement();   
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }


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
                    Debug.Log(enemyStat.spawnPosition + " Knockback Force : " + currentKnockbackForce);
                }

                StartCoroutine(KnockbackStart());
            }
            else if (ModuleManager.SINGLETON.knockbackAcquired == 2)
            {
                Debug.Log("Knockback 2");
                currentKnockbackForce = knockbackData[1].knockbackForce;
                StartCoroutine(KnockbackStart());
            }
            else if (ModuleManager.SINGLETON.knockbackAcquired == 3)
            {
                Debug.Log("Knockback 3");
                currentKnockbackForce = knockbackData[2].knockbackForce;
                StartCoroutine(KnockbackStart());
            }
        }
        else
        {
            Debug.Log(enemyStat.spawnPosition + " Max Knockbacks Reached");
            currentKnockbackStep = 0;
        }
    }

    IEnumerator KnockbackStart()
        {
            if (ModuleManager.SINGLETON.knockbackAcquired > 0)
            {
//                Debug.Log(gameObject.name + " Knockback");
                isKnockedBack = true;
                yield return new WaitForSeconds(0.5f);
                isKnockedBack        = false;
                currentKnockbackStep = 0;
                //Debug.Log(enemyStat.spawnPosition + " Knockback Movement Finished");
            }
        }
    void KnockbackMovement()
    {
        Vector2 knockbackDirection = (rb.position - (Vector2) player.transform.position).normalized;
        rb.linearVelocity = knockbackDirection * currentKnockbackForce;
    }
}
