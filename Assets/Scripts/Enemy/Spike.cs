using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(other.gameObject){
            other.gameObject.GetComponent<HealthP1>().TakeDamage(10);    
            other.gameObject.GetComponent<KnockbackManager>().knockback(8,other.gameObject.transform.position-transform.position);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
     }

     
}
