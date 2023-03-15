using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponFireScript : MonoBehaviour
{
    [SerializeField] private float attackRate = 0.2f;
    [SerializeField] private GameObject attack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float freezeAmt;
    private GameObject player;
    private Rigidbody2D playerRb;
    private bool canAttack = true;
    private float attackCooldown = 0.0f;

    void Start(){
        player = GameObject.FindWithTag("Player");
        playerRb = player.GetComponent<Rigidbody2D>();
    }
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
        if(context.performed&&canAttack) {
            player.SendMessage("FreezeInputs",freezeAmt);
            Instantiate(attack,firePoint.position,firePoint.rotation);
            attackCooldown+= attackRate;
            canAttack = false;   
        } 
    }
    public void setAttack(GameObject attack){
        this.attack = attack;
    }
    public void setFireRate(float attackRate){
        this.attackRate = attackRate;
    }
    public void setFreezeTime(float freezeAmt){
        this.freezeAmt = freezeAmt;
    }
    public void setCooldown(float cd){
        attackCooldown = cd;
    }
    public float getCooldown(){
        return attackCooldown;
    }
}
