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
    
     private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==6){
            TakeDamage(5);
        }
     }

    
}
