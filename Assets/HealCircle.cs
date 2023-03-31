using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCircle : MonoBehaviour
{
    [SerializeField] float healRate;
    void OnTriggerStay2D(Collider2D other){
        if(other.tag=="Player"){
            PlayerPersistence.Instance.GetComponent<HealthP1>().TakeDamage(-healRate*Time.deltaTime);
        }
    }
}
