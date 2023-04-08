using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float force;
    public float timeBetweenDash;
    private float reactionTime = 1f;
    private bool canDash = true;
    private float dashCooldown =0f;
    private SpriteRenderer sr;
    void Start(){
        player = PlayerPersistence.Instance;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    

    void Update(){
        if(dashCooldown > 0){
            dashCooldown -= Time.deltaTime;
        }
        else{
            canDash = true;
        }
        if(reactionTime > 0){
            reactionTime -= Time.deltaTime;
        }
        if(Vector2.Distance(player.transform.position,transform.position)<5&&canDash){
            
            Vector2 travel = (player.transform.position - transform.position).normalized * force;
            rb.velocity = new Vector2(travel.x,0f);
            canDash = false;
            dashCooldown = timeBetweenDash;
            //sr.color = new Color(154,0,135,255);
        }
        if (rb.velocity.x > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            
        } else if (rb.velocity.x < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            
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
