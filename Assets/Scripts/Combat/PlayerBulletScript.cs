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
    public bool wallClipping = false;
    [SerializeField] bool contactDamage = true;
    private Transform player;
    private ComboManager cm;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        cm = player.gameObject.GetComponent<ComboManager>();
        rb = GetComponent<Rigidbody2D>();
        if(rb){
        rb.velocity = transform.right*bulletSpeed;    
        }
        
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
    public void setStats(float spd, float dmg, float deathTime, bool multiTarget, bool wallClipping, bool contactDamage){
        this.bulletDamage = dmg;
        this.bulletSpeed = spd;
        this.bulletDeathTime = deathTime;
        this.multiTarget = multiTarget;
        this.wallClipping = wallClipping;
        this.contactDamage = contactDamage;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(contactDamage){
        if(other.gameObject.layer==6){
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage*cm.getComboDamageMultiplier());
            cm.increaseHitcount(1);
            if(multiTarget){
            bulletDeathTime/=1.25f;
            }else{
            Destroy(gameObject); 
            }
        }else if(other.gameObject.tag!="Player"&&!wallClipping&&other.gameObject.layer!=7){
             Destroy(gameObject);
        }    
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(contactDamage){
        if(other.gameObject.layer==6){
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage*cm.getComboDamageMultiplier());
            cm.increaseHitcount(1);
            if(multiTarget){
            bulletDeathTime/=1.25f;
            }else{
            Destroy(gameObject); 
            }
        }else if(other.gameObject.tag!="Player"&&!wallClipping&&other.gameObject.layer!=7){
             Destroy(gameObject);
        }    
        }
        
    }
}
