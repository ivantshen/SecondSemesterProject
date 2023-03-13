using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy33 : MonoBehaviour
{
    public bool inPlayer;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            inPlayer = true;
            
             
        }
        else{
            inPlayer = false;
            

        }
     }
}
