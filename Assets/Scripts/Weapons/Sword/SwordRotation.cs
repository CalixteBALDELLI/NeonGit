using System;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SwordRotation : MonoBehaviour
{
    //public Vector3 startingPosition;
    [HideInInspector]
    public float                  startingPosition;
    [HideInInspector]
    public float                  targetPosition;
    [HideInInspector]
    public float                  timeCount;
    [HideInInspector]
    public float                  currentRotation;
    
    public WeaponScriptableObject swordData;
    

    void Update() // rotation de l'épée du point de départ vers le point d'arrivée à une certaine vitesse.
    {
        currentRotation            = Mathf.Lerp(startingPosition, targetPosition, timeCount);
        transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        timeCount                  = timeCount += swordData.Speed * Time.deltaTime;
    }
}

