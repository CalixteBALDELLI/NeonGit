using System.Collections;
using UnityEngine;

public class PropagationCollider : MonoBehaviour
{
    [SerializeField] PropagationScript propagationScript;
    [SerializeField] private EnemyStat enemyStat;
    Vector3                            spawnPosition;

    void OnTriggerEnter2D(Collider2D other) // Ajoute dans une liste tous les ennemis présents dans la HitBox.
    {

        //Debug.Log(propagationScript.spawnPosition + " Has Detected : " + other.name);
        propagationScript.focusedEnemies.Add(other.GetComponent<EnemyStat>());
    }

    public IEnumerator Propagate()
    {
        yield return new WaitForFixedUpdate();
        propagationScript.PropagationSetup();
    }
}
