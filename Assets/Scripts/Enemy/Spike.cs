using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(other.gameObject){
            other.gameObject.GetComponent<HealthP1>().TakeDamage(5);    
            other.gameObject.GetComponent<KnockbackManager>().knockback(8,other.gameObject.transform.position-transform.position);
            }
        }
     }

     
}
