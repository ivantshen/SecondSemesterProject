using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openShopUI : MonoBehaviour
{
    public GameObject gameObject;
    bool active = false;
    public string shopKey;
    private bool test = true;
    
    public IEnumerator openAndClose(){
        if(active == false){
            gameObject.transform.gameObject.SetActive(true);
            active = true;
        }
        else if(active==true){
            gameObject.transform.gameObject.SetActive(false);
            active = false;
        }
        yield return new WaitForSeconds(3f);
        test = true;
    }
    

    private void OnTriggerStay2D(Collider2D other){
        //&& Input.GetKeyDown("g")
        if(other.tag == "Player" ){
            if(test){
            test = false;
            StartCoroutine(openAndClose());
        }
        
            //openAndClose();
            //Debug.Log("works");
        }
        
    }


    //void Update()
    //{
        //if(Input.GetKeyDown("g")){
         //  openAndClose();
        //}
    //}
}
