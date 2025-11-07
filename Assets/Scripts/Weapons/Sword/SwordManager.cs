	using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class SwordManager : MonoBehaviour
{
    //public SwordRotation  playerSword;
    public GameObject     playerSwordGameObject;
    public PlayerMovement player;
    public float          swingCardinalRadius;
    public float          swingDiagonalRadius;

    [HideInInspector]
    public float swingRadiusDividedbyTwo;

    bool rotationActivated = true;

    [HideInInspector]
    public float startingPosition;
    [HideInInspector]
    public float targetPosition;
    [HideInInspector]
    public float timeCount;
    [HideInInspector]
    public float currentRotation;

    public WeaponScriptableObject swordData;

    [SerializeField] KnifeController projectile;
    //public GameObject             swordParent;


    public IEnumerator SwordAttack() // Active l'épée et règle la direction de son coup.
    {

        playerSwordGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        playerSwordGameObject.SetActive(true);
        SwordDirection();
        rotationActivated = true;
        // projectile.LaunchProjectile(); // LANCE LE PROJECTILE A CHAQUE COUP D'EPEE.
    }

    void SwordMovement() // rotation de l'épée du point de départ vers le point d'arrivée à une certaine vitesse.
    {
        currentRotation            = Mathf.Lerp(startingPosition, targetPosition, timeCount);
        transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        timeCount                  = timeCount += swordData.Speed * Time.deltaTime;
    }

    void Update()
    {
        if (rotationActivated)
        {
            SwordMovement();
        }

        if (currentRotation == targetPosition) // si l'épée a terminé son coup, retour à son état d'origine, puis démarrage de la Coroutine pour la réactiver.
        {
            currentRotation   = 0;
            timeCount         = 0;
            rotationActivated = false;
            StartCoroutine(SwordAttack());
        }
    }


    void SetPlayerSwordOrientation(float startingPos, float swingRadius) // réglage du point de départ et du point d'arivée de sa rotation.
    {
        startingPosition = startingPos;
        targetPosition   = startingPosition + swingRadius;

        //if (startingPosition < 90)
        {
        }
        //else
        {
            //playerSword.targetPosition = startingPosition + swingRadius;
        }
    }


    void SwordDirection() // Detection de la direction de déplacement de l'avatar.
    {


        if (player.lastHorizontalVector == -1)
        {
            SetPlayerSwordOrientation(135, swingCardinalRadius); //Gauche
        }

        if (player.lastHorizontalVector == 1)
        {
            SetPlayerSwordOrientation(315, swingCardinalRadius); // Droite
        }

        if (player.lastVerticalVector == -1)
        {
            SetPlayerSwordOrientation(225, swingCardinalRadius); // Bas
        }

        if (player.lastVerticalVector == 1)
        {
            SetPlayerSwordOrientation(45, swingCardinalRadius); // Haut
        }

        // Diagonal Detection

        if (player.lastHorizontalVector < 0 && player.lastHorizontalVector > -1 && player.lastVerticalVector > 0 && player.lastVerticalVector < 1) // Gauche Haut
        {
            SetPlayerSwordOrientation(90, swingDiagonalRadius);
        }

        if (player.lastHorizontalVector < 0 && player.lastHorizontalVector > -1 && player.lastVerticalVector < 0 && player.lastVerticalVector > -1) // Gauche Bas
        {
            SetPlayerSwordOrientation(225, swingDiagonalRadius);
        }

        if (player.lastHorizontalVector > 0 && player.lastHorizontalVector < 1 && player.lastVerticalVector > 0 && player.lastVerticalVector < 1) // Droite Haut
        {
            SetPlayerSwordOrientation(45, swingDiagonalRadius);
        }

        if (player.lastHorizontalVector > 0 && player.lastHorizontalVector < 1 && player.lastVerticalVector < 0 && player.lastVerticalVector > -1) // Droite Bas
        {
            SetPlayerSwordOrientation(270, swingDiagonalRadius);
        }
    }
}

    
