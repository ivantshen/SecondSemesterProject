using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject player;
    
    public Transform EnemyTwo;
    public Rigidbody2D rb;
    public float direction;

    private float distance;
    
    private void OnTriggerStay2D(Collider2D other){
        if(rb.velocity == new Vector2(0f,0f)){
        if(other.tag == "Player"){
            //EnemyTwo.position = new Vector2(-5f, 0);
            rb.velocity = new Vector2(direction, 0f);
            
        }
        }
    }
    

   
}
