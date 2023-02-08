using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public float comboTimeLimit;
    private float comboResetTime;
    public ComboDisplay comboDisplay;
    private float comboDamageMultiplier;
    private int comboLevel;
    private int hitCount;

    void Update(){
        if(comboResetTime>0){
            comboResetTime-=Time.deltaTime;
        }else if(hitCount>0){
            comboLevel = 0;
            hitCount = 0;
            comboDamageMultiplier = 1;
            comboDisplay.setComboText("");
        }
    }
    public float getComboDamageMultiplier(){
        return comboDamageMultiplier;
    }
    public void increaseHitcount(int num){
        comboResetTime = comboTimeLimit;
        hitCount+= num;
        comboDisplay.setComboText(hitCount.ToString());
        if(hitCount>=125){
            comboLevel = 7; //SSS
            comboDamageMultiplier = 2.5f;
        }else if(hitCount>=75){
            comboLevel = 6; //SS
            comboDamageMultiplier = 2;
        }else if(hitCount>=50){
            comboLevel = 5; //S
            comboDamageMultiplier = 1.75f;
        }else if(hitCount>=30){
            comboLevel = 4; //A
            comboDamageMultiplier = 1.5f;
        }else if(hitCount>=15){
            comboLevel = 3; //B
             comboDamageMultiplier = 1.25f;
        }else if(hitCount>=5){
            comboLevel = 2;//C
            comboDamageMultiplier = 1.1f;
        }else{
            comboLevel = 1;//D
            comboDamageMultiplier = 1;
        }
    }
}
