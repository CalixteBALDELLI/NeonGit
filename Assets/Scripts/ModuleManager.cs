using System;
using System.Collections;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    public bool bite = true;
    

    public void Propagation()
    {
      if (bite == true)
          {
             Debug.Log("Propagation");
          }  
    }
    
    
    


    
    
    public void TirEnergie()
    {
        Debug.Log("tire energie");
    }
}
