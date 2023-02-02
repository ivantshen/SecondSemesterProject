using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject player;
    
    public Transform EnemyTwo;
    public Rigidbody2D rb;
    public float force;
    public float timeBetweenDash;
    private float distance;
    private bool canDash = true;
    private float dashCooldown;

    public Collider2D[] colliderList; 

    

    void Update(){
        if(dashCooldown > 0){
            dashCooldown -= Time.deltaTime;
        }
        else{
            canDash = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other){
        
        if(other.tag == "Player" && canDash){
            //EnemyTwo.position = new Vector2(-5f, 0);
            
            rb.velocity = (other.transform.position - transform.position).normalized * force;
            rb.velocity = new Vector2(rb.velocity.x,0f);
            canDash = false;
            dashCooldown = timeBetweenDash;
        }
        
    }
    

   
}
