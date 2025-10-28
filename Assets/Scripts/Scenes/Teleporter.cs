using Unity.VisualScripting;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject canvas;
    public void OnTriggerEnter2D(Collider2D boxCollider)
    {
        Debug.Log("Canva");
        Instantiate(canvas);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
