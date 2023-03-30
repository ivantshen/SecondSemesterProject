using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : ParentHealth
{
    
    
    public static EnemyHealth keyyy;
    [SerializeField] private bool allowComboInc = true;

    public bool getAllowCombo(){
        return allowComboInc;
    }

    
    
}
