using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SwordAttack : MonoBehaviour
{
    //public Vector3 startingPosition;
    public SwordManager           swordManager;
    public PlayerMovement         player;
    public float                  startingPosition;
    public float                  targetPosition;
    public float                  speed;
    public float                  timeCount;
    public float                  currentRotation;
    public WeaponScriptableObject swordData;
    

    void Update()
    {
        currentRotation            = Mathf.Lerp(startingPosition, targetPosition, timeCount);
        transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        timeCount                  = timeCount += speed * Time.deltaTime;
    }
}

