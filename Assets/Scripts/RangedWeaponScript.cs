using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;

public class RangedWeaponScript : MonoBehaviour
{
    public float knockbackForce;
    public GameObject hitEffect;
    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
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
        player = GameObject.FindWithTag("Player").transform;
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
        if(timeBetweenAttacks>0){
           timeBetweenAttacks-= Time.deltaTime;
        }
    }
    public void slashAttack(InputAction.CallbackContext context){
        if(timeBetweenAttacks<=0){
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
    }
    IEnumerator delayedSlash(float delay,int slashDamage){
        yield return new WaitForSeconds(delay);
        Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(firePoint.position,attackRange,targetLayer);
        Collider2D[] enemies2ToDmg = Physics2D.OverlapCircleAll(firePoint2.position,attackRange,targetLayer);
        Collider2D[] enemies3ToDmg = Physics2D.OverlapCircleAll(firePoint2.position,attackRange,targetLayer);

        foreach (Collider2D enemy in enemiesToDmg)
        {
        if(enemy){
        GameObject newVFX = Instantiate(hitEffect,enemy.ClosestPoint(transform.position),enemy.transform.rotation) as GameObject;
        Destroy(newVFX,2);
        enemy.GetComponent<KnockbackManager>().knockback(knockbackForce,(enemy.transform.position-transform.position).normalized);
        enemy.GetComponent<EnemyHealth>().TakeDamage(slashDamage*cm.getComboDamageMultiplier());
        if(enemy.GetComponent<EnemyHealth>().getAllowCombo()){
        cm.increaseHitcount(1);    
        }
        }
        }

        
    }
    //void OnDrawGizmosSelected() {
       // Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(firePoint.position,attackRange);
    //}
}
