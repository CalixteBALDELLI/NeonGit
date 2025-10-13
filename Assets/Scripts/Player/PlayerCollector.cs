using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        //Regarde si l'autre game object a l'interface ICollectible
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //Si c'est le cas, appel la methode collect
            collectible.Collect();
            Debug.Log("collectible collected");
        }
    }
}
