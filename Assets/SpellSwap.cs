using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellSwap : MonoBehaviour
{
    [SerializeField] private GameObject[] spells;
    [SerializeField] private float[] spellCooldown;
    private int spellIndex = 0;
    private WeaponFireScript wp;

    void Start(){
        wp = GetComponent<WeaponFireScript>();
        wp.setAttack(spells[spellIndex]);
        wp.setFireRate(spellCooldown[spellIndex]);
        wp.setFreezeTime(spellCooldown[spellIndex]*0.8f);
    }
    public void swapSpell(InputAction.CallbackContext context){
        if(context.performed){
        if(spellIndex<spells.Length-1){
            spellIndex++;
        }else{
            spellIndex =0;
        }
        wp.setAttack(spells[spellIndex]);
        wp.setFireRate(spellCooldown[spellIndex]);    
        wp.setFreezeTime(spellCooldown[spellIndex]*0.8f);
        }
        
    }
}
