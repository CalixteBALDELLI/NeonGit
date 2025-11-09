using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordActivator : MonoBehaviour
{
    [SerializeField] SwordManager swordManager;
    [SerializeField] GameObject swordParent;
    [SerializeField] GameObject player;
    [SerializeField] GameObject instantiatedSword;
    void Start()
    {
        StartCoroutine(SwordAttack());
    }
    public IEnumerator SwordAttack() // Active l'épée et règle la direction de son coup.
    {

        //Destroy(gameObject);
        yield return new WaitForSeconds(1);
        Instantiate(swordParent, transform.position, Quaternion.identity, player.transform);
        instantiatedSword = GameObject.FindGameObjectWithTag("PlayerSword");
        swordManager = instantiatedSword.GetComponent<SwordManager>();
        Debug.Log("Epee");
        int currentRotationInt = (int) swordManager.currentRotation;
        int targetPositionInt  = (int) swordManager.targetPosition;
        if (currentRotationInt == targetPositionInt) // si l'épée a terminé son coup, retour à son état d'origine, puis démarrage de la Coroutine pour la réactiver.
        {
            Destroy(instantiatedSword);
            StartCoroutine(SwordAttack());
        }
        // projectile.LaunchProjectile(); // LANCE LE PROJECTILE A CHAQUE COUP D'EPEE.
    }

    void Update()
    {
        
    }
}
