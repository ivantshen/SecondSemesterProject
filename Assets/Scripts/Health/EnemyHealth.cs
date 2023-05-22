using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : ParentHealth
{
    
    
    public static EnemyHealth keyyy;
    [SerializeField] private bool allowComboInc = true;
    //private FireBaseLeaderboard lb;

    public bool getAllowCombo(){
        return allowComboInc;
    }

    
    
    
    
}
