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
    [SerializeField] private float minFallSpeed;
    [SerializeField] private float maxFallSpeed;
    private Collider2D col;
    private float finalDamage;
    private float finalForce;
    private float finalKnockback;
    private float finalFallSpeed;
    private bool dying = false;
    private bool awake = false;
    private Rigidbody2D rb;
    private ComboManager cm;
    private float finalRot;
    private bool allowPosStop = true;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        cm = PlayerPersistence.Instance.GetComponent<ComboManager>();
        col = GetComponent<Collider2D>();
        col.enabled = false;
        if(piercing){
            allowPosStop = false;
        }
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
        transform.up =Vector3.Slerp(transform.up, rb.velocity.normalized, Time.deltaTime*finalFallSpeed);    
        }
        
    }
    // Start is called before the first frame update
    public void assignStatsAndFire(float chargeValue,Vector3 dir){
        transform.SetParent(null);
        finalDamage = (chargeValue*(maximumDamage-minimumDamage) + minimumDamage);
        finalForce = chargeValue*(arrowMaximumForce-arrowMinimumForce) + arrowMinimumForce;
        finalKnockback = chargeValue*(maximumKnockback-minimumKnockback) + minimumKnockback;
        finalFallSpeed = chargeValue*(maxFallSpeed-minFallSpeed) + minFallSpeed;
        rb.isKinematic = false;
        col.enabled = true;
        awake = true;
        rb.AddForce(finalForce*dir);
    }
    void OnTriggerEnter2D(Collider2D other){
        if(awake){
        if(other.gameObject.layer==6){
            other.GetComponent<EnemyHealth>().TakeDamage(finalDamage*cm.getComboDamageMultiplier());
            if(other.GetComponent<EnemyHealth>().getAllowCombo()){
            cm.increaseHitcount(1);    
            }
            if(allowPosStop){
                dying = true;    
                awake = false;
                Destroy(rb);
                transform.parent = other.transform;
                other.gameObject.GetComponent<KnockbackManager>().knockback(finalForce,(other.transform.position-transform.position).normalized);
            }
            numTargets--;
            if(numTargets<=0){
                allowPosStop = true;
            }
        }else{
            dying = true;    
            awake = false;
            Destroy(rb);
        }
        }
        
    }
}
