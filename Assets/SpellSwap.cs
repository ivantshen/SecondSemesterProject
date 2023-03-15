using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellSwap : MonoBehaviour
{
    [SerializeField] private GameObject[] spells;
    [SerializeField] private float[] spellCooldown;
    [SerializeField] private float[] playerFreezeTime;
    private float[] stashedCooldowns;
    private int spellIndex = 0;
    private WeaponFireScript wp;

    void Start(){
        stashedCooldowns = new float[3];
        wp = GetComponent<WeaponFireScript>();
        wp.setAttack(spells[spellIndex]);
        wp.setFireRate(spellCooldown[spellIndex]);
        wp.setFreezeTime(playerFreezeTime[spellIndex]);
        wp.setCooldown(stashedCooldowns[spellIndex]);
    }
    void Update(){
        for(int i=0;i<stashedCooldowns.Length;i++){
            if(stashedCooldowns[i]>0){
                stashedCooldowns[i]-=Time.deltaTime;
            }
        }
    }
    public void swapSpell(InputAction.CallbackContext context){
        if(context.performed){
            stashedCooldowns[spellIndex] = wp.getCooldown();
        if(spellIndex<spells.Length-1){
            spellIndex++;
        }else{
            spellIndex =0;
        }
        wp.setAttack(spells[spellIndex]);
        wp.setFireRate(spellCooldown[spellIndex]);    
        wp.setFreezeTime(playerFreezeTime[spellIndex]);
        wp.setCooldown(stashedCooldowns[spellIndex]);
        }
        
    }
}
