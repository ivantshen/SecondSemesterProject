using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float bulletDeathTime = 5f;

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
        }
        //else if(other.gameObject.tag!="Player"&&!wallClipping&&other.gameObject.layer!=7){
            // Destroy(gameObject);
        //}
    }
}
