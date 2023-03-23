using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    public float comboTimeLimit;
    private float comboResetTime;
    private ComboDisplay comboDisplay;
    private float comboDamageMultiplier;
    private int comboLevel =0;
    private int hitCount =0;

    void Start(){
        comboDisplay = GameObject.FindWithTag("ComboUI").GetComponent<ComboDisplay>();
    }
    void Update(){
        if(comboResetTime>0){
            comboResetTime-=Time.deltaTime;
        }else if(hitCount>0){
            comboLevel = 1;
            hitCount = 0;
            comboDamageMultiplier = 1;
            comboDisplay.resetComboText();
        }
    }
    public float getComboDamageMultiplier(){
        return comboDamageMultiplier;
    }
    public int getHitCount(){
        return hitCount;
    }
    public void decreaseHitCount(int num){
        hitCount-= num;
        if(hitCount>125){
            comboLevel = 7; //SSS
            comboDamageMultiplier = 4.50f;//75f + (0.025f*((hitCount-125)/2));
        }else if(hitCount>75){
            comboLevel = 6; //SS
            comboDamageMultiplier = 3.50f;//f //+ (0.0225f*(hitCount-75));
        }else if(hitCount>50){
            comboLevel = 5; //S
            comboDamageMultiplier = 2.50f;//f //+ (0.02f*(hitCount-50));
        }else if(hitCount>30){
            comboLevel = 4; //A
            comboDamageMultiplier = 2.0f;//f //+ (0.0175f*(hitCount-30));
        }else if(hitCount>15){
            comboLevel = 3; //B
             comboDamageMultiplier = 1.60f;//575f //+ (0.015f*(hitCount-15));
        }else if(hitCount>5){
            comboLevel = 2;//C
            comboDamageMultiplier = 1.25f;//f //+(0.0125f*(hitCount-5));
        }else{
            comboLevel = 1;//D
            comboDamageMultiplier = 1; //+ (0.01f*hitCount);
        }
        comboDisplay.setComboText(hitCount.ToString(),comboLevel,Mathf.Round(comboDamageMultiplier*100.0f)*0.01f);
    }
    public void increaseHitcount(int num){
        comboResetTime = comboTimeLimit;
        hitCount+= num;
        
        if(hitCount>125){
            comboLevel = 7; //SSS
            comboDamageMultiplier = 4.50f;//75f + (0.025f*((hitCount-125)/2));
        }else if(hitCount>75){
            comboLevel = 6; //SS
            comboDamageMultiplier = 3.50f;//f //+ (0.0225f*(hitCount-75));
        }else if(hitCount>50){
            comboLevel = 5; //S
            comboDamageMultiplier = 2.50f;//f //+ (0.02f*(hitCount-50));
        }else if(hitCount>30){
            comboLevel = 4; //A
            comboDamageMultiplier = 2.0f;//f //+ (0.0175f*(hitCount-30));
        }else if(hitCount>15){
            comboLevel = 3; //B
             comboDamageMultiplier = 1.60f;//575f //+ (0.015f*(hitCount-15));
        }else if(hitCount>5){
            comboLevel = 2;//C
            comboDamageMultiplier = 1.25f;//f //+(0.0125f*(hitCount-5));
        }else{
            comboLevel = 1;//D
            comboDamageMultiplier = 1; //+ (0.01f*hitCount);
        }
        comboDisplay.setComboText(hitCount.ToString(),comboLevel,Mathf.Round(comboDamageMultiplier*100.0f)*0.01f);
    }
}
