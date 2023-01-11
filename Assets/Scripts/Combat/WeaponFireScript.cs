using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFireScript : MonoBehaviour
{
    public string fireKey;
    public float attackRate = 0.5f;
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
        if(canAttack&&Input.GetKeyDown(fireKey)){
            Instantiate(attack,firePoint.position,firePoint.rotation);
            attackCooldown+= attackRate;
            canAttack = false;
        }
    }
}
