using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPulse : MonoBehaviour
{
    [SerializeField] private int pulseDamage;
    [SerializeField] private float pulseRadius;
    [SerializeField] private float knockbackForce;
    private LayerMask targetLayer;
    private Transform player;
    private ComboManager cm;
    private float cd;
    [SerializeField] private float cdValue = 1;
    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
        targetLayer = LayerMask.GetMask("Enemies");
        cm = player.gameObject.GetComponent<ComboManager>();
    }
    
    void Update(){
        if(cd>0){
            cd-=Time.deltaTime;
        }else{
            cd= cdValue;
            Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(transform.position,pulseRadius,targetLayer);
            foreach (Collider2D enemy in enemiesToDmg){
                if(enemy){
                    enemy.GetComponent<KnockbackManager>().knockback(knockbackForce,(enemy.ClosestPoint(transform.position)-(Vector2)transform.position).normalized);
                    enemy.GetComponent<EnemyHealth>().TakeDamage(pulseDamage*cm.getComboDamageMultiplier());
                    if(enemy.GetComponent<EnemyHealth>().getAllowCombo()){
                    cm.increaseHitcount(1);    
                    }
                }
            }
        }
    }
    
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,pulseRadius);
    }
}
