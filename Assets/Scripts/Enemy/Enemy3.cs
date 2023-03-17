using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
     public int TeleportTime;
     private bool test = true;
    float disX = 0;
    float disY = 0;
    //public CircleCollider2D circle;
    private GameObject player;
    public GameObject check;
    private float distance;
    //public float yMin;
    //public float yMax;
    //public float xMin;
    //public float xMax;

    //for attacking/shooting
    private bool canAttack = true;
    private float attackCooldown = 0.0f;
    [SerializeField] private float attackRate = 0.2f;
    [SerializeField] private GameObject attack;
    [SerializeField] private Transform firePoint;
    

    void Start(){
        player = GameObject.FindWithTag("Player");
    }

    private IEnumerator Move(){
        yield return new WaitForSeconds(TeleportTime);
        Check();
        yield return new WaitForSeconds(1);
        
        Teleport();
        yield return new WaitForSeconds(0.5f);
        Fire();
        yield return new WaitForSeconds(0.5f);
        Fire();
        yield return new WaitForSeconds(1f);
        Fire();
        yield return new WaitForSeconds(0.5f);
        Fire();
        test = true;
    }

    void Teleport(){
        
        
        

        transform.position = new Vector2(Mathf.Clamp(disX, -18, 8),Mathf.Clamp(disY, 0, 3));
     
    }

    void Check(){
        RandomNum();
        check.transform.position = new Vector2(Mathf.Clamp(disX, -18, 8),Mathf.Clamp(disY, 0, 3));
        if(player){
        distance = Vector2.Distance(new Vector2(Mathf.Clamp(disX, -18, 8),Mathf.Clamp(disY, 0, 3)), player.transform.position);
        }
        if(distance < 3){
            Debug.Log("it doenst work");
            Check();
        }

    }

    void Fire(){
        //if(canAttack) {
            //player.SendMessage("FreezeInputs",freezeAmt);
            Instantiate(attack,firePoint.position,firePoint.rotation);
            //attackCooldown+= attackRate;
            //nAttack = false;   
       // } 
       Debug.Log("Fire");
    }

    
    void RandomNum(){
        disX = Random.Range(-18f,-8f);
        disY = Random.Range(0f,3f);
    }
    private void OnCollisionStay2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(other.gameObject){
            other.gameObject.GetComponent<HealthP1>().TakeDamage(20);    
            }
             
        }
     }

    // Update is called once per frame
    void Update()
    {
      
        if(test){
            test = false;
            StartCoroutine(Move());
        }
        if(attackCooldown>0.0f){
            attackCooldown-=Time.deltaTime;
        }else{
            canAttack = true;
        }
        
    }
}
