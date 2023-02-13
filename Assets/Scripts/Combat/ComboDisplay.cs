using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboDisplay : MonoBehaviour
{
    public TMP_Text tm;
    public TMP_Text multiplier;
    private int lastHitCount = 0;
    public void setComboText(string txt,int level,float comboDamageMultiplier){
        tm.text = txt;
        if(level==1){
        tm.color = new Color(50,50,50,1);
        }else if(level==2){
        tm.color = new Color(75,75,75,1);
        }else if(level==3){
        tm.color = new Color(100,100,100,1);
        }else if(level==4){
        tm.color = new Color(125,125,125,1);
        }else if(level==5){
        tm.color = new Color(150,150,150,1);
        }else if(level==6){
        tm.color = new Color(200,200,200,1);
        }else{
        tm.color = new Color(255,255,255,1);
        }
        multiplier.text = "x " + comboDamageMultiplier.ToString();
        StartCoroutine(increaseSize(tm,0.75f,0.8f));
        StartCoroutine(increaseSize(multiplier,0.5f,0.8f));
    }
    public void resetComboText(){
        tm.text = "";
        multiplier.text ="";
    }
    IEnumerator increaseSize(TMP_Text text, float amount, float time){
        text.fontSize += amount;
        yield return new WaitForSeconds(time);
        text.fontSize -=amount;
    }
}
