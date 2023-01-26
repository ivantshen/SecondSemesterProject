using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthP1 : ParentHealth
{
   

    public void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }
    }

    
    
     

    
}
