using System;
using UnityEngine;

public class WeaponChoiceUiTrigger : MonoBehaviour
{
    public GameObject canvas;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("weaponENTer");
        canvas.SetActive(true);
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
