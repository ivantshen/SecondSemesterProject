using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponSlashingScript : MonoBehaviour
{
    //public Animation anim;
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
    private float comboResetTime = 2f;
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
            if(Input.GetKeyDown("f")){
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
        GameObject newSlash = Instantiate(slash[index],firePoint.position,player.rotation,null);
        Destroy(newSlash,0.25f);
        Collider2D[] enemiesToDmg = Physics2D.OverlapCircleAll(firePoint.position,attackRange,targetLayer);
        foreach (Collider2D enemy in enemiesToDmg)
        {
        StartCoroutine(delayedHitEffect(0.05f,enemy));
        if(slashNum ==2){
        StartCoroutine(delayedHitEffect(0.25f,enemy));
        }
        }
        if(slashNum<numAttacks-1){
        slashNum++;    
        }else{
        slashNum = 0;
        }
        timeBetweenAttacks = slashRate*((-slashNum*0.25f)+1);
        comboResetTime = slashRate*1.2f;
    }
    IEnumerator delayedHitEffect(float delay, Collider2D enemy){
        yield return new WaitForSeconds(delay);
        if(enemy){
        GameObject newVFX = Instantiate(hitEffect,enemy.ClosestPoint(transform.position),enemy.transform.rotation) as GameObject;
        Destroy(newVFX,2);
        enemy.GetComponent<EnemyHealth>().TakeDamage(slashDamage);    
        }
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position,attackRange);
    }
}