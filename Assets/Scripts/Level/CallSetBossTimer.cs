using UnityEngine;

public class CallSetBossTimer : MonoBehaviour
{
    public void SetBossTimer()
    {
        Timer.SINGLETON.SetBossTimer(180);
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
