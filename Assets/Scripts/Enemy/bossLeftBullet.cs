using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossLeftBullet : MonoBehaviour
{
    public float bulletDeathTime;
    [SerializeField] GameObject explosion;
    private Rigidbody2D rb;
    public float bulletSpeed;
        private GameObject player;


    public float speed = 20f;

    private float distance;

    
    void Start(){
        player = GameObject.FindWithTag("Player");

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right*speed;
    }

    void Update()
    {

        if(bulletDeathTime>0){
            bulletDeathTime-= Time.deltaTime;
        }else{
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            other.gameObject.GetComponent<HealthP1>().TakeDamage(20);    
            Destroy(gameObject); 
            Instantiate(explosion,transform.position,Quaternion.identity,null);
        }
        else if(other.gameObject.layer != 6){
            Destroy(gameObject);
        }
        else{
        Instantiate(explosion,transform.position,Quaternion.identity,null);
        }
        //else if(other.gameObject.tag!="Player"&&!wallClipping&&other.gameObject.layer!=7){
            // Destroy(gameObject);
        //}
    }
}

