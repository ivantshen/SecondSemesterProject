using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
     public int TeleportTime;
     private bool test = true;
     float disX = 0;
    float disY = 0;

    private IEnumerator Move(){
        yield return new WaitForSeconds(TeleportTime);
        Teleport();
        test = true;
    }

    void Teleport(){
        RandomNum();
        //while(disX >=9 || disX <=-9 || disY >=-3 || disX <= -7){
           // RandomNum();
       // }
    //if(Input.GetKeyDown(KeyCode.U)){
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + disX, -9, 9),Mathf.Clamp(transform.position.y + disY, -1, 4));
     //}
    }

    void RandomNum(){
        float disX = Random.Range(-9f,9f);
        float disY = Random.Range(-7f,-3f);
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
