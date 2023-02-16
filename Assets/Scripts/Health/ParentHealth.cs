using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentHealth : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth {get; private set;}
    public int monAmt;

    private void Awake(){
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage){
        
        if (currentHealth > 0){
            currentHealth = Mathf.Clamp(currentHealth - _damage,0 , startingHealth);
            if(currentHealth <=0){
                Destroy(gameObject);
                
                XPManager.instance.AddXP(monAmt);
            }
        }
        else{
            
        }
    }
    
}
