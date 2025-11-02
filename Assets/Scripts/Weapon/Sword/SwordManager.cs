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
    public float          swingCardinalRadius;
    public float          swingDiagonalRadius;
    public float          swingRadiusDividedbyTwo;
    
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
    void SetPlayerSwordOrientation(float startingPosition, float swingRadius)
    {
        playerSword.startingPosition = startingPosition;
        playerSword.targetPosition = startingPosition + swingRadius;
        //if (startingPosition < 90)
        { 
        }
        //else
        {
            //playerSword.targetPosition = startingPosition + swingRadius;
        }
    }

    
    void SwordDirection()
    {
            // Horizontal and Vertical Detection
        
        if (player.lastHorizontalVector == -1) 
        { 
            SetPlayerSwordOrientation(135,swingCardinalRadius); //Gauche
        }

        if (player.lastHorizontalVector == 1) 
        { 
            SetPlayerSwordOrientation(315,swingCardinalRadius); // Droite
        }

        if (player.lastVerticalVector == -1) 
        {
            SetPlayerSwordOrientation(225,swingCardinalRadius); // Bas
        }

        if (player.lastVerticalVector == 1) 
        {
            SetPlayerSwordOrientation(45,swingCardinalRadius); // Haut
        }

            // Diagonal Detection
            
        if (player.lastHorizontalVector < 0 && player.lastHorizontalVector > -1 && player.lastVerticalVector > 0 && player.lastVerticalVector < 1) // Gauche Haut
        {
            SetPlayerSwordOrientation(90,swingDiagonalRadius);
        }

        if (player.lastHorizontalVector < 0 && player.lastHorizontalVector > -1 && player.lastVerticalVector < 0 && player.lastVerticalVector > -1) // Gauche Bas
        {
            SetPlayerSwordOrientation(225,swingDiagonalRadius);
        }

        if (player.lastHorizontalVector > 0 && player.lastHorizontalVector < 1 && player.lastVerticalVector > 0 && player.lastVerticalVector < 1) // Droite Haut
        {
            SetPlayerSwordOrientation(45,swingDiagonalRadius);
        }

        if (player.lastHorizontalVector > 0 && player.lastHorizontalVector < 1 && player.lastVerticalVector < 0 && player.lastVerticalVector > -1) // Droite Bas
        {
            SetPlayerSwordOrientation(270,swingDiagonalRadius);
        } 
        
    }
}
    
