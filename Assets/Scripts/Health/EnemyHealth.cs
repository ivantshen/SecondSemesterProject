using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private float startingHealth;
    public float currentHealth {get; private set;}
    public GameObject damageText;

    private void Awake(){
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage){
        
        if (currentHealth > 0){
            currentHealth = Mathf.Clamp(currentHealth - _damage,0 , startingHealth);
            if(currentHealth <=0){
                Destroy(gameObject);
            }
        }
        else{
            
        }
    }
    private void OnCollisionEnter2D(Collision2D other){

    }
}
