	using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SwordManager : MonoBehaviour
{
    //public SwordRotation  playerSword;
    [SerializeField] private GameObject     playerSwordGameObject;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float          swingCardinalRadius;
    [SerializeField] private float          swingDiagonalRadius;
    [SerializeField] WeaponScriptableObject swordData;
    [SerializeField] KnifeController        projectile;

    private bool  rotationActivated;
    Vector3       swordDirection;
    private float targetPosition = -45;
    private float currentRotation;
    private float timeCount;
    private float angle = 45f;
    private float swingRadiusDividedbyTwo;
    private bool  diagonalMovement;

    
    void Start()
    {
        StartCoroutine(SwordAttack()); //premier coup d'épée
    }
    
    public IEnumerator SwordAttack() // Active l'épée et règle la direction de son coup.
    {

        playerSwordGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        playerSwordGameObject.SetActive(true);
        NewSetPlayerSwordOrientation();
        rotationActivated = true;
        projectile.LaunchProjectile(); // LANCE LE PROJECTILE A CHAQUE COUP D'EPEE.
    }
    void NewSetPlayerSwordOrientation() //règle la direction de son coup.
    {
        swordDirection             = player.lastMovedVector;
        angle                      =  Mathf.Atan2(swordDirection.y, swordDirection.x) * Mathf.Rad2Deg;
        angle                      += 45f;
        transform.localEulerAngles =  new Vector3(0, 0, angle);
        targetPosition             =  angle - swingCardinalRadius + 1;
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

    
