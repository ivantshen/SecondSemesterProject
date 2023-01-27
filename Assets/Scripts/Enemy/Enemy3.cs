using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
     public int TeleportTime;
     private bool test = true;

    private IEnumerator Move(){
        yield return new WaitForSeconds(TeleportTime);
        Teleport();
        test = true;
    }

    void Teleport(){
    float disX = Random.Range(-10f,10f);
    float disY = Random.Range(-5f,5f);
    //if(Input.GetKeyDown(KeyCode.U)){
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + disX, -30, 10),Mathf.Clamp(transform.position.y + disY, 0, 5));
     //}
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
