using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SwordManager : MonoBehaviour
{
    public SwordAttack    playerSword;
    public GameObject     playerSwordGameObject;
    public PlayerMovement player;
    
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

        Debug.Log(player.moveDirection.x);
        Debug.Log(player.moveDirection.y);
        
        

        if (player.moveDirection.x == -1) //Gauche
        {
            playerSword.startingPosition = 135;
            playerSword.targetPosition   = 225;
        }

        if (player.moveDirection.x == 1) // Droite
        {
            playerSword.startingPosition = 45;
            playerSword.targetPosition   = -45;
        }

        if (player.moveDirection.y == -1) // Bas
        {
            playerSword.startingPosition = 225;
            playerSword.targetPosition   = 315;
        }

        if (player.moveDirection.y == 1) // Haut
        {
            playerSword.startingPosition = 45;
            playerSword.targetPosition   = 135;
        }
        
        

        if (player.moveDirection.x > 0 && player.moveDirection.y > 0) //Haut Droite
        {
            playerSword.startingPosition = 90;
            playerSword.targetPosition   = 45;
        }

        if (player.moveDirection.x < 0 && player.moveDirection.y > 0) //Haut Gauche
        {
            playerSword.startingPosition = 90;
            playerSword.targetPosition   = 135;
        }

        if (player.moveDirection.x < 0 && player.moveDirection.y < 0) //Bas Gauche
        {
            playerSword.startingPosition = 225;
            playerSword.targetPosition   = 270;
        }

        if (player.moveDirection.x > 0 && player.moveDirection.y < 0) //Bas Droite
        {
            playerSword.startingPosition = 270;
            playerSword.targetPosition   = 315;
        }

       
        //StopCoroutine(SwordAttack());

        Debug.Log("Sword ON");
        
    }
}
    
