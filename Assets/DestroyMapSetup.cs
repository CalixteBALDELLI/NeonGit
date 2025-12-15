using UnityEngine;

public class DestroyMapSetup : MonoBehaviour
{
    [SerializeField] GameObject mapSetup;

    public void mapSetupDestroy()
    {
        Destroy(mapSetup);
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
