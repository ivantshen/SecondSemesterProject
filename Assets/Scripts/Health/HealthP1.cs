using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthP1 : MonoBehaviour
{
   
    [SerializeField] private float iFrameTime;

    public float startingHealth;
    public float currentHealth {get; private set;}

    private void Awake(){
        currentHealth = startingHealth;
        Physics2D.IgnoreLayerCollision(0,6,false);
    }

    public void TakeDamage(float _damage){
        
        if (currentHealth > 0){
            currentHealth = Mathf.Clamp(currentHealth - _damage,0 , startingHealth);
            StartCoroutine(Invulnerability());
            if(currentHealth <=0){
                Destroy(gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Physics2D.IgnoreLayerCollision(0,6,false);
            }
        }
        else{
            
        }
    }

    private IEnumerator Invulnerability(){
        Physics2D.IgnoreLayerCollision(0,6,true);
        yield return new WaitForSeconds(iFrameTime);
        Physics2D.IgnoreLayerCollision(0,6,false);
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Spike"){
            
            if(other.gameObject){
              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
              
            }
             
             
        }
     }

    

    
    
     

    
}
