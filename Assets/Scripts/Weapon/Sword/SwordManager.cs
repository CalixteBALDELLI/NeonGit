using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public SwordAttack playerSword;
    public GameObject playerSwordGameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //StartCoroutine(SwordAttack());
    }

    void Update()
    {
        if (playerSword.currentRotation == playerSword.targetPosition)
        {
            playerSword.currentRotation = 0;
            playerSword.timeCount       = 0;
            StartCoroutine(SwordAttack());
        }
    }
    
    public IEnumerator SwordAttack()
    {
        Debug.Log("Sword OFF");
        playerSwordGameObject.transform.localEulerAngles = new Vector3(0, 0, playerSword.startingPosition);
        playerSwordGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        playerSwordGameObject.SetActive(true);
        StopCoroutine(SwordAttack());
            
        Debug.Log("Sword ON");
    }
}
