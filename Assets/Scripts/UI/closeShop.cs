using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeShop : MonoBehaviour
{
    public GameObject gameObject;
    bool active = false;
    //public string shopKey;
    private bool test = true;
    
    private void Close(){
       
        gameObject.transform.gameObject.SetActive(false);
        
        
    }
    

    private void OnTriggerStay2D(Collider2D other){
        //&& Input.GetKeyDown("g")
        if(other.tag == "Player" ){
            Close();
        }
        
            //openAndClose();
            //Debug.Log("works");
        }
        
    }


