using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float minimumDamage;
    [SerializeField] private float maximumDamage;
    [SerializeField] private float arrowMinimumForce;
    [SerializeField] private float arrowMaximumForce;
    [SerializeField] private float minimumKnockback;
    [SerializeField] private float maximumKnockback;
    [SerializeField] private bool piercing;
    [SerializeField] private float percentDamageLossPerTargetHit;
    [SerializeField] private int numTargets;
    [SerializeField] private float timeTillDeath;
    [SerializeField] private Collider2D collisionCollider;
    private float finalDamage;
    private float finalForce;
    private float finalKnockback;
    private bool dying = false;
    private bool awake = false;
    private Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        collisionCollider.enabled = false;
    }
    void Update(){
        if(dying){
            if(timeTillDeath>0){
                timeTillDeath-=Time.deltaTime;
            }else{
                Destroy(gameObject);
            }
        }
        if(awake){
        transform.up =Vector3.Slerp(transform.up, rb.velocity.normalized, Time.deltaTime*3);    
        }
        
    }
    // Start is called before the first frame update
    public void assignStatsAndFire(float chargeValue,Vector3 dir){
        transform.SetParent(null);
        finalDamage = chargeValue*(maximumDamage-minimumDamage) + minimumDamage;
        finalForce = chargeValue*(arrowMaximumForce-arrowMinimumForce) + arrowMinimumForce;
        finalKnockback = chargeValue*(maximumKnockback-minimumKnockback) + minimumKnockback;
        rb.isKinematic = false;
        awake = true;
        if(!piercing){
        collisionCollider.enabled = true;    
        }
        rb.AddForce(finalForce*dir);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(awake){
        if(other.gameObject.layer==6){
            other.GetComponent<EnemyHealth>().TakeDamage(finalDamage);
            numTargets--;
            if(numTargets<=0){
                collisionCollider.enabled = true;
            }
        }else{
            collisionCollider.enabled = true;
        }    
        }
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(awake){
        dying = true;    
        awake = false;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        Destroy(rb);
        Destroy(collisionCollider);
        if(other.gameObject.layer==6){
            other.gameObject.GetComponent<KnockbackManager>().knockback(finalForce,(other.transform.position-transform.position).normalized);
            transform.parent = other.transform;
            rb.velocity = Vector2.zero;
        }
        }
        
    }
}
