	using System;
    using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SwordManager : SwordActivator
{
    
    public float                                    swingCardinalRadius;
    public float                                    swingDiagonalRadius;
    bool                                            diagonalMovement = false;
    public                   float                  angle;
    public                   Vector3                swordDirection;
    [HideInInspector]        PlayerMovement         player;
    [HideInInspector] public float                  swingRadiusDividedbyTwo;
    [HideInInspector] public float                  targetPosition;
    [HideInInspector] public float                  timeCount;
    [HideInInspector] public float                  currentRotation;
    [SerializeField]         WeaponScriptableObject swordData;
    SwordActivator swordActivator;
    [HideInInspector]        KnifeController        projectile;
    //public GameObject             swordParent;

    

    
    public void Start()
    {
        player                     =  FindFirstObjectByType<PlayerMovement>();
        swordActivator        = FindFirstObjectByType<SwordActivator>();
        projectile                 =  FindFirstObjectByType<KnifeController>();
        swordDirection             =  player.lastMovedVector;
        angle                      =  Mathf.Atan2(swordDirection.y, swordDirection.x) * Mathf.Rad2Deg;
        angle                      += 45f;
        transform.localEulerAngles =  new Vector3(0, 0, angle);
        targetPosition             =  angle - swingCardinalRadius;
        Debug.Log("ajustement");
    }

    void Update()
    {
        currentRotation            = Mathf.Lerp(angle, targetPosition, timeCount);
        transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        timeCount                  = timeCount += swordData.Speed * Time.deltaTime;
    }
    
}

    
