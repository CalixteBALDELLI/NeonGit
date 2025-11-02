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
    public float          lastPlayerX;
    public float          lastPlayerY;
    
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
        
        playerSwordGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        playerSwordGameObject.SetActive(true);
        SwordDirection();
        
    }
    void SetPlayerSwordOrientation(float startingPosition, float targetPosition)
    {
        playerSword.startingPosition = startingPosition;
        playerSword.targetPosition   = targetPosition;
    }

    
    void SwordDirection()
    {
            // Horizontal and Vertical Detection
        if (player.lastHorizontalVector == -1) //Gauche
        { 
            SetPlayerSwordOrientation(135,235);
        }

        if (player.lastHorizontalVector == 1) // Droite
        { 
            SetPlayerSwordOrientation(45,-45);
        }

        if (player.lastVerticalVector == -1) // Bas
        {
            SetPlayerSwordOrientation(225,315);
        }

        if (player.lastVerticalVector == 1) // Haut
        {
            SetPlayerSwordOrientation(45,135);
        }

            // Diagonal Detection
            
        if (player.lastHorizontalVector < 0 && player.lastHorizontalVector > -1 && player.lastVerticalVector > 0 && player.lastVerticalVector < 1) // Gauche Haut
        {
            SetPlayerSwordOrientation(90,135);
        }

        if (player.lastHorizontalVector < 0 && player.lastHorizontalVector > -1 && player.lastVerticalVector < 0 && player.lastVerticalVector > -1) // Gauche Bas
        {
            SetPlayerSwordOrientation(225,270);
        }

        if (player.lastHorizontalVector > 0 && player.lastHorizontalVector < 1 && player.lastVerticalVector > 0 && player.lastVerticalVector < 1) // Droite Haut
        {
            SetPlayerSwordOrientation(90,45);
        }

        if (player.lastHorizontalVector > 0 && player.lastHorizontalVector < 1 && player.lastVerticalVector < 0 && player.lastVerticalVector > -1) // Droite Bas
        {
            SetPlayerSwordOrientation(270,315);
        } 

        void DiagonalSword()
        {
            if (player.lastHorizontalVector > 0 && player.lastVerticalVector > 0) //Haut Droite
            {
                playerSword.startingPosition = 90;
                playerSword.targetPosition   = 45;
            }

            if (player.lastHorizontalVector < 0 && player.lastVerticalVector > 0) //Haut Gauche
            {
                playerSword.startingPosition = 90;
                playerSword.targetPosition   = 135;
            }

            if (player.lastHorizontalVector < 0 && player.lastVerticalVector < 0) //Bas Gauche
            {
                playerSword.startingPosition = 225;
                playerSword.targetPosition   = 270;
            }

            if (player.lastHorizontalVector > 0 && player.lastVerticalVector < 0) //Bas Droite
            {
                playerSword.startingPosition = 270;
                playerSword.targetPosition   = 315;
            }
        }
    }
}
    
