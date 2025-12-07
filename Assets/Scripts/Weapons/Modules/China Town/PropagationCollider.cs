using System.Collections;
using UnityEngine;

public class PropagationCollider : MonoBehaviour
{
    [SerializeField] PropagationScript propagationScript;
    Vector3                            spawnPosition;
    public           bool              enemiesAdded;
    
    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {
        Debug.Log(propagationScript.spawnPosition + " Has Detected : " + other.name);
        //propagationScript.detectedEnemy = other.GetComponent<EnemyStat>();
        propagationScript.focusedEnemies.Add(other.GetComponent<EnemyStat>()); 
        //enemiesAdded = true;
    }
    
    public IEnumerator CheckColliderActivation()
    {
        Debug.Log(spawnPosition + "Checked Collider");
        do
        {
            yield return null;
            //Debug.Log(spawnPosition + " Collider Not Effective");

        } while (enemiesAdded == false);
        Debug.Log(propagationScript.spawnPosition + " EnemiesAdded : " + enemiesAdded);
        propagationScript.DistanceBetweenEnemies();
    }

    public IEnumerator Propagate()
    {
        yield return new WaitForFixedUpdate();
        propagationScript.PropagationSetup();
    }
}
