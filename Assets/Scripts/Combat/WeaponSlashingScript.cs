using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponSlashingScript : MonoBehaviour
{
    //public Animation anim;
    public string fireKey;
    public float knockbackForce;
    public GameObject hitEffect;
    public Transform firePoint;
    public float slashRate;
    private float timeBetweenAttacks;
    public float attackRange;
    public int slashDamage;
    private bool resetPosition;
    private LayerMask targetLayer;
    public GameObject[] slash;
    public Transform player;
    private int slashNum = 0;
    private int numAttacks = 3;
    public float freezeAmt = 0.2f;
    private float comboResetTime = 2f;
    private ComboManager cm;
    // Start is called before the first frame update
    void Start()
    {
        /*anim = transform.GetChild(1).gameObject.GetComponent<Animation>();
         foreach (AnimationState state in anim)
        {
            state.speed = 1.75F;
        }
        */
        targetLayer = LayerMask.GetMask("Enemies");
    }

    // Update is called once per frame
    void Update()
    {
        if(comboResetTime>0){
            comboResetTime-=Time.deltaTime;
        }else{
            slashNum = 0;
        }
        if(timeBetweenAttacks<=0){
            /*
            if(anim){
              anim.Play("idle",PlayMode.StopAll);  
            }
            */
            if(Input.GetKeyDown(fireKey)){
                slashAttack(slashNum);
                /*
                if(anim){
                  anim.Play("slash", PlayMode.StopAll);  
                }
                */
                
            }
        }else{
            timeBetweenAttacks-= Time.deltaTime;
        }
    }
    private void slashAttack(int index){
        if(cm==null){
            cm = transform.parent.GetComponent<ComboManager>();
        }
        player.SendMessage("FreezeInputs",freezeAmt);
        GameObject newSlash = Instantiate(slash[index],firePoint.position,player.rotation,null);
        Destroy(newSlash,0.25f);
        
        StartCoroutine(delayedSlash(0.05f));
        if(slashNum ==2){
        StartCoroutine(delayedSlash(0.25f));
        }
        if(slashNum<numAttacks-1){
        slashNum++;    
        }else{
        slashNum = 0;
        }
        timeBetweenAttacks = slashRate*((-slashNum*0.25f)+1);
        comboResetTime = slashRate*1.2f;
    }
    IEnumerator delayedSlash(float delay){
        Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(firePoint.position,attackRange,targetLayer);
        yield return new WaitForSeconds(delay);
        foreach (Collider2D enemy in enemiesToDmg)
        {
        if(enemy){
        GameObject newVFX = Instantiate(hitEffect,enemy.ClosestPoint(transform.position),enemy.transform.rotation) as GameObject;
        Destroy(newVFX,2);
        enemy.GetComponent<KnockbackManager>().knockback(knockbackForce,(enemy.transform.position-transform.position).normalized);
        enemy.GetComponent<EnemyHealth>().TakeDamage(slashDamage*cm.getComboDamageMultiplier());
        cm.increaseHitcount(1);
        }
        }
        
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position,attackRange);
    }
}