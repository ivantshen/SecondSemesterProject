using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] int explosionDamage;
    [SerializeField] float explosionRadius;
    [SerializeField] float knockbackForce;
    private LayerMask targetLayer;
    private Transform player;
    private ComboManager cm;
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
        targetLayer = LayerMask.GetMask("Enemies");
        cm = player.gameObject.GetComponent<ComboManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag!="Player"){
        Instantiate(explosion,transform.position,Quaternion.identity,null);
        Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(transform.position,explosionRadius,targetLayer);
        foreach (Collider2D enemy in enemiesToDmg)
        {
        if(enemy){
        enemy.GetComponent<KnockbackManager>().knockback(knockbackForce,(enemy.ClosestPoint(transform.position)-(Vector2)transform.position).normalized);
        enemy.GetComponent<EnemyHealth>().TakeDamage(explosionDamage*cm.getComboDamageMultiplier());
        cm.increaseHitcount(1);
        }
    }
    }   
    }
    
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag!="Player"){
        Instantiate(explosion,transform.position,Quaternion.identity,null);
        Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(transform.position,explosionRadius,targetLayer);
        foreach (Collider2D enemy in enemiesToDmg)
        {
        if(enemy){
        enemy.GetComponent<KnockbackManager>().knockback(knockbackForce,(enemy.ClosestPoint(transform.position)-(Vector2)transform.position).normalized);
        enemy.GetComponent<EnemyHealth>().TakeDamage(explosionDamage*cm.getComboDamageMultiplier());
        cm.increaseHitcount(1);
        }
    }
    }   
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
