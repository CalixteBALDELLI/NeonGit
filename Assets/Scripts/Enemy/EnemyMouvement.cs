using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouvement : MonoBehaviour
{
    public           EnemyScriptableObject enemyData;
    public           Transform             player;
    public           EnemyStat             enemyStat;
    public           Rigidbody2D           rb;
    public           bool                  isStunned;
    [SerializeField] bool                  DONOTMOVE;
    public           bool                  isKnockedBack;
    [SerializeField] KnockBackModule                        knockbackModule;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PlayerStats.SINGLETON.playerTransform;
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
            knockbackModule.KnockbackMovement();   
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}

