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
         
        if(Input.GetKeyDown(KeyCode.W)){
        transform.position=new Vector2(transform.position.x,transform.position.y+distance); 
        }
        else if(Input.GetKeyDown(KeyCode.D)){
        transform.position=new Vector2(transform.position.x,transform.position.y-distance); 
        }
    }
}
