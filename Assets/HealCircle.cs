using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCircle : MonoBehaviour
{
    [SerializeField] float healRate;
    [SerializeField] float outsideRadius;
    [SerializeField] bool healOrDmg;
    void OnTriggerStay2D(Collider2D other){
        if(healOrDmg){
            if(Vector2.Distance(other.transform.position,transform.position)>outsideRadius&&other.tag=="Player"){
                PlayerPersistence.Instance.GetComponent<HealthP1>().TakeDamage(-healRate*Time.deltaTime);
            }
        }else{
             if(Vector2.Distance(other.transform.position,transform.position)>outsideRadius&&other.tag=="Player"){
                PlayerPersistence.Instance.GetComponent<HealthP1>().TakeDamage(healRate*Time.deltaTime);
            }
        }
        
    }
}
