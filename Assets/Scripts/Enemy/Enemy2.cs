using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public float timeBetweenDash;
    private bool canDash = true;
    private float dashCooldown =0f;
    void Start(){
        player = PlayerPersistence.Instance;
        rb = GetComponent<Rigidbody2D>();
    }
    

    void Update(){
        if(dashCooldown > 0){
            dashCooldown -= Time.deltaTime;
        }
        else{
            canDash = true;
        }
        if(Vector2.Distance(player.transform.position,transform.position)<5&&canDash){
            Vector2 travel = (player.transform.position - transform.position).normalized * force;
            rb.velocity = new Vector2(travel.x,0f);
            canDash = false;
            dashCooldown = timeBetweenDash;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
             if(other.gameObject){
             other.gameObject.GetComponent<HealthP1>().TakeDamage(10);     
             }
             
        }
     }

   
}
