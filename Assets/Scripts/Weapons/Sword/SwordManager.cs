using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;
using Vector3 = UnityEngine.Vector3;

public class SwordManager : MonoBehaviour
{
    //public SwordRotation  playerSword;
    [SerializeField] private GameObject      playerSwordGameObject;
    [SerializeField] private PlayerMovement  player;
    [SerializeField]         PlayerStats     playerStats;
    [SerializeField] public  float           swingCardinalRadius;
    [SerializeField]         KnifeController projectile;
    [SerializeField]         ModuleManager   moduleManager;
    [SerializeField]         bool            logValues;
    
    private bool  rotationActivated;
    Vector3       swordDirection;
    private float targetPosition = -45;
    private float currentRotation;
    private float timeCount;
    private float angle = 45f;
    private float swingRadiusDividedbyTwo;
    float         currentSwingSpeed;
    
    void Start()
    {
        StartCoroutine(SwordAttack()); //premier coup d'épée
    }
    
    public IEnumerator SwordAttack() // Active l'épée et règle la direction de son coup.
    {

        playerSwordGameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        playerSwordGameObject.SetActive(true);
        SetPlayerSwordOrientation();
        rotationActivated = true;
        if (moduleManager.projectileAcquired)
        {
            Debug.Log("Projectile launched");
            projectile.LaunchProjectile(); // LANCE LE PROJECTILE A CHAQUE COUP D'EPEE.
        } 
    }
    
    void SetPlayerSwordOrientation() // Règle la direction du coup d'épée.
    {
        swordDirection             = player.lastMovedVector;
        angle                      =  Mathf.Atan2(swordDirection.y, swordDirection.x) * Mathf.Rad2Deg;
        
        swingRadiusDividedbyTwo =  swingCardinalRadius / 2;
        angle                   -= swingRadiusDividedbyTwo; // Divise par deux l'angle du point de départ de l'épée pour qu'elle passe devant le centre du personnage.
        angle                   += swingCardinalRadius;     // Angle auquel l'avatar lève l'épée
        
        targetPosition             =  angle - swingCardinalRadius + 1;
        
        if (player.lastMovedVector.x == -1 || player.lastMovedVector.y == -1 || player.lastMovedVector.x is < 0 and > -1 && player.lastMovedVector.y is < 1 and > 0 || player.lastMovedVector.x is < 0 and > -1 && player.lastMovedVector.y is < 0 and > -1) // conserver le même point de départ entre les différentes directions.
        {
            angle          -= swingCardinalRadius;
            targetPosition += swingCardinalRadius;
        }
        
        transform.localEulerAngles =  new Vector3(0, 0, angle);
    }
    
    void SwordMovement() // rotation de l'épée du point de départ vers le point d'arrivée à une certaine vitesse.
    {
        currentRotation            = Mathf.Lerp(angle, targetPosition, timeCount);
        transform.localEulerAngles = new Vector3(0, 0, currentRotation);
        timeCount                  = timeCount += playerStats.currentSwordSwingSpeed * Time.deltaTime;
    }

    void Update()
    {
        if (logValues)
        { 
            Debug.Log("Angle = " + angle + " Target Position = " + targetPosition + " Time Count = " + timeCount + " Current Rotation = " + currentRotation + " Sword Direction = " + swordDirection + " Swing Radius Divided by Two = " + swingRadiusDividedbyTwo);
        }

        
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
            StartCoroutine(SwordAttack());
        }
    }
}

    
