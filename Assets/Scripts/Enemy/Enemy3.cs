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
        if(disX >=9 || disX <=-9 || disY <=-1 || disX >= 4){
            //RandomNum();
            Debug.Log("Ovangay");
        }
        Debug.Log(disX);
                Debug.Log(disY);

            //if(Input.GetKeyDown(KeyCode.U)){
        transform.position = new Vector2(Mathf.Clamp(disX, 0, 10),Mathf.Clamp(disY, -1, 1));
     //}
    }

    void RandomNum(){
        disX = Random.Range(0f,10f);
        disY = Random.Range(-1f,1f);
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
