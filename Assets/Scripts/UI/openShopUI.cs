using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openShopUI : MonoBehaviour
{
    public GameObject gameObject;
    bool active = false;
    public string shopKey;
    
    public void openAndClose(){
        if(active == false){
            gameObject.transform.gameObject.SetActive(true);
            active = true;
        }
        else{
            gameObject.transform.gameObject.SetActive(false);
            active = false;
        }
    }

    //private void OnTriggerStay2D(Collider2D other){
        
        //if(other.tag == "Player" && Input.GetKeyDown("g")){
          //  openAndClose();
          //  Debug.Log("works");
        //}
        
   // }


    void Update()
    {
        if(Input.GetKeyDown("g")){
           openAndClose();
        }
    }
}
