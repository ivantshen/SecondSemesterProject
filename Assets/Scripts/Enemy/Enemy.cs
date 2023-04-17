using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;
    public float speed;
    private Vector3 startPos;
    private Vector3 targetPos;
    public Vector3 moveOffset;

    private float distance;

    void Start(){
        player = GameObject.FindWithTag("Player");
        startPos = transform.position;
        targetPos = startPos;
    }
    // Update is called once per frame
    void Update()
    {
        if(player){
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        //
        
        if(distance < 7){
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else{
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

                if(transform.position == targetPos){
                if(targetPos == startPos){
                    targetPos = startPos + moveOffset;
            
             }
             else{
                 targetPos = startPos;
             }
                }
                transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
    }
    }
    private void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(other.gameObject){
            other.gameObject.GetComponent<HealthP1>().TakeDamage(10);    
            }
             
        }
     }
}
