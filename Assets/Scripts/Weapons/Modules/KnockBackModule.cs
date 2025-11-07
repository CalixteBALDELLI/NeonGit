using System.Collections; 
using UnityEngine;

public class KnockBackModule : MonoBehaviour
{
    public EnemyMouvement ennemyMovement;
    
    public IEnumerator Knockback()
    {
        ennemyMovement.isKnockedBack = true;
        yield return new WaitForSeconds(0.5f);
        ennemyMovement.isKnockedBack     = false;
    }
}
