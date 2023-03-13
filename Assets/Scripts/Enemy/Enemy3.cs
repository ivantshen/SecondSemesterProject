using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy33
{
     public int TeleportTime;
     private bool test = true;
    float disX = 0;
    float disY = 0;
    public CircleCollider2D circle;
    private GameObject player;
    public GameObject check;

    

    void Start(){
        player = GameObject.FindWithTag("Player");
    }

    private IEnumerator Move(){
        yield return new WaitForSeconds(TeleportTime);
        Check();
        yield return new WaitForSeconds(1);
        
        Teleport();
        test = true;
    }

    void Teleport(){
        
        
        
        

        


        //if(inPlayer){
           // Debug.Log("In player");
            
       // }
        transform.position = new Vector2(Mathf.Clamp(disX, -18, 8),Mathf.Clamp(disY, 0, 3));
     
    }

    void Check(){
        RandomNum();
        //if(disX >=8 || disX <=-8 || disY <=0 || disX >= 3){
            //RandomNum();
            //Debug.Log("Ovangay");
        //}
        //circle.transform.position = new Vector2(Mathf.Clamp(disX, -18, 8),Mathf.Clamp(disY, 0, 3));
        
        //Vector2.Distance(player.position,check.position);
        //if()
        //Physics2D.OverlapCircleAll(transform.position,pickupRange,weaponLayer);




        //if(inPlayer){
           // Debug.Log("In player");
            
        //}
        //else{
            //Debug.Log("Not in player prolly");
        //}
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
        





        //if(Input.GetKeyDown(KeyCode.W)){
        //transform.position=new Vector2(transform.position.x,transform.position.y+distance); 
        //}
        //else if(Input.GetKeyDown(KeyCode.D)){
        //transform.position=new Vector2(transform.position.x,transform.position.y-distance); 
        //}
    }
}
