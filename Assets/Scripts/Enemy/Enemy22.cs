using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy22 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other){
        Debug.Log("It works?");
        if(other.tag == "Player"){
             other.gameObject.GetComponent<ParentHealth>().TakeDamage(10);
        }
     }
}
