using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthP1 : MonoBehaviour
{
   [SerializeField]private float startingHealth;
    public float currentHealth {get; private set;}

    private void Awake(){
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage){
        currentHealth = Mathf.Clamp(currentHealth - _damage,0 , startingHealth);
        if (currentHealth > 0){
            //player hurt
        }
        else{
            //player dead
        }
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            TakeDamage(1);
        }
    }
}
