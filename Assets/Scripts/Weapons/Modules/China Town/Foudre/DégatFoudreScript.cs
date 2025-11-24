using UnityEngine;

public class DegatFoudre : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        Destroy(other.gameObject);
        
    }
    
}