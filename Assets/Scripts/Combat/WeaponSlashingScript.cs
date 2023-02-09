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
    private float timeBetweenAttacks;
    public float attackRange;
    private bool resetPosition;
    private LayerMask targetLayer;
    public GameObject[] slash;
    public float[] slashRate;
    public int[] hitsPerSlash;
    public int[] damagePerSlash;
    public float[] delayBetweenSlash;
    public float timeToComboReset;
    public Transform player;
    private int slashNum = 0;
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
                slashAttack();
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
    private void slashAttack(){
        if(cm==null){
            cm = transform.parent.GetComponent<ComboManager>();
        }
        player.SendMessage("FreezeInputs",freezeAmt);
        GameObject newSlash = Instantiate(slash[slashNum],firePoint.position,player.rotation,null);
        Destroy(newSlash,0.25f);
        for(int i=0;i<hitsPerSlash[slashNum];i++){
        StartCoroutine(delayedSlash(delayBetweenSlash[slashNum]*(i+1),damagePerSlash[slashNum]));    
        }
        if(slashNum<slash.Length-1){
        slashNum++;    
        }else{
        slashNum = 0;
        }
        timeBetweenAttacks = slashRate[slashNum];
        comboResetTime = timeToComboReset;
    }
    IEnumerator delayedSlash(float delay,int slashDamage){
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