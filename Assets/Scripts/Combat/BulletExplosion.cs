using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] int explosionDamage;
    [SerializeField] float explosionRadius;
    [SerializeField] float knockbackForce;
    [SerializeField] private bool wallClip;
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
                if(other.gameObject.layer ==8&&wallClip){
                    return;
                }
                Vector2 explosionLocation = other.ClosestPoint(transform.position);
                Instantiate(explosion,explosionLocation,Quaternion.identity,null);
                Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(explosionLocation,explosionRadius,targetLayer);
            foreach (Collider2D enemy in enemiesToDmg)
            {
                if(enemy){
                    enemy.GetComponent<KnockbackManager>().knockback(knockbackForce,(enemy.ClosestPoint(explosionLocation)-(Vector2)explosionLocation).normalized);
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
