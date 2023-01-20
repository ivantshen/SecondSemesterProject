using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
     public float distance=10;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        float disX = Random.Range(-10f,10f);
        float disY = Random.Range(-50f,5f);
        if(Input.GetKeyDown(KeyCode.U)){
            transform.position = new Vector2(Mathf.Clamp(transform.position.x + disX, -30, 10),Mathf.Clamp(transform.position.y + disY, 0, 5));
        }





        //if(Input.GetKeyDown(KeyCode.W)){
        //transform.position=new Vector2(transform.position.x,transform.position.y+distance); 
        //}
        //else if(Input.GetKeyDown(KeyCode.D)){
        //transform.position=new Vector2(transform.position.x,transform.position.y-distance); 
        //}
    }
}
