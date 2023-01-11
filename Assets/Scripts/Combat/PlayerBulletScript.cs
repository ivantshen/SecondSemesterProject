using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed = 10f;
    public float bulletDamage = 10f;
    public float bulletDeathTime = 5f;
    public bool multiTarget = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right*bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(bulletDeathTime>0){
            bulletDeathTime-= Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==6){
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        }
        if(other.gameObject.tag!="Player"&&!multiTarget){
            Destroy(gameObject);  
        }else if(multiTarget){
            bulletDeathTime/=1.0f;
        }
    }
}
