using System;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    //public Vector3 startingPosition;
    public float targetPosition;
    public float     speed;
    public float     timeCount;
    public float currentRotation;
    
    
    void Update()
    {
        if (currentRotation > targetPosition)
        {
        currentRotation = Mathf.Lerp(0, targetPosition, timeCount);
        transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        timeCount                             = timeCount += speed * Time.deltaTime;;
        }
        
    }
}

