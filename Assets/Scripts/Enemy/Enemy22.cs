using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy22 : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
             if(other.gameObject){
             other.gameObject.GetComponent<HealthP1>().TakeDamage(10);     
             }
             
        }
     }
}
