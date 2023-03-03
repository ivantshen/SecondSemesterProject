using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFireScript : MonoBehaviour
{
    public float attackRate = 0.2f;
    public GameObject attack;
    public Transform firePoint;
    private bool canAttack = true;
    private float attackCooldown = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if(attackCooldown>0.0f){
            attackCooldown-=Time.deltaTime;
        }else{
            canAttack = true;
        }
    }
    public void fire(InputAction.CallbackContext context){
        if(canAttack){
            Instantiate(attack,firePoint.position,firePoint.rotation);
            attackCooldown+= attackRate;
            canAttack = false;   
        }
    }
}
