using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
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
        //rb.velocity = transform.right*bulletSpeed;
    }

    void Update()
    {

        if(bulletDeathTime>0){
            bulletDeathTime-= Time.deltaTime;
        }else{
            Destroy(gameObject);
        }

        //rb.velocity = transform.right*bulletSpeed;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        //

        //if(distance < 10){
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        //}

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
