using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAiming : MonoBehaviour
{
    private Transform player;
    void Start(){
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update(){
        if(Input.GetAxis("Vertical")!=0){
            if(player.transform.rotation.z==180){
                transform.localRotation = Quaternion.Euler(0,180,90*Input.GetAxisRaw("Vertical"));
            }else{
                transform.localRotation = Quaternion.Euler(0,0,90*Input.GetAxisRaw("Vertical"));
            }
        }
    }
}
