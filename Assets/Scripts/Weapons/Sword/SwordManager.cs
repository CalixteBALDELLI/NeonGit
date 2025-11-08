	using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SwordManager : MonoBehaviour
{
    //public SwordRotation  playerSword;
    public GameObject     playerSwordGameObject;
    public PlayerMovement player;
    public float          swingCardinalRadius;
    public float          swingDiagonalRadius;
    bool                  diagonalMovement = false;
    public float                 angle;
    Vector3 swordDirection;

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
        NewSetPlayerSwordOrientation(); //SwordDirection();
        rotationActivated = true;
        // projectile.LaunchProjectile(); // LANCE LE PROJECTILE A CHAQUE COUP D'EPEE.
    }
    void NewSetPlayerSwordOrientation()
    {
        swordDirection             = player.lastMovedVector;
        angle                      =  Mathf.Atan2(swordDirection.y, swordDirection.x) * Mathf.Rad2Deg;
        angle                      += 45f;
        transform.localEulerAngles =  new Vector3(0, 0, angle);
        targetPosition             =  angle - swingCardinalRadius;
    }

    
    void SwordMovement() // rotation de l'épée du point de départ vers le point d'arrivée à une certaine vitesse.
    {
        currentRotation            = Mathf.Lerp(angle, targetPosition, timeCount);
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
            transform.localEulerAngles = new Vector3(0, 0, 0);
            currentRotation            = 0;
            timeCount                  = 0;
            rotationActivated          = false;
            diagonalMovement           = false;
            StartCoroutine(SwordAttack());
        }
    }
}

    
