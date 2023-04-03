using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComboDisplay : MonoBehaviour
{
    public TMP_Text tm;
    public TMP_Text multiplier;
    public Slider[] sliders; //0-5 (6 sliders total)
    public int previousLevel;
    public void setComboText(string txt,int level,float comboDamageMultiplier){
        tm.text = txt;
        
        int hitCount = int.Parse(txt);
        if(level==1){
        tm.color = new Color(50,50,50,1);
        sliders[0].value = hitCount;
        }else if(level==2){
        tm.color = new Color(75,75,75,1);
        sliders[1].value = hitCount;
        }else if(level==3){
        tm.color = new Color(100,100,100,1);
        sliders[2].value = hitCount;
        }else if(level==4){
        tm.color = new Color(125,125,125,1);
        sliders[3].value = hitCount;
        }else if(level==5){
        tm.color = new Color(150,150,150,1);
        sliders[4].value = hitCount;
        }else if(level==6){
        tm.color = new Color(200,200,200,1);
        if(hitCount<126){
        sliders[5].value = hitCount;    
        }
        resetAboveLevel(level);
        }else{
        tm.color = new Color(255,255,255,1);
        }
        multiplier.text = "x " + comboDamageMultiplier.ToString();
        StartCoroutine(increaseSize(tm,0.75f,0.8f));
        StartCoroutine(increaseSize(multiplier,0.5f,0.8f));
        if(level>previousLevel){
        StartCoroutine(increaseSize(tm,2.75f,2.3f));
        StartCoroutine(increaseSize(multiplier,2.25f,2.3f));
        }
        previousLevel = level;
    }
    private void resetAboveLevel(int comboLvl){
        for(int i=comboLvl;i<sliders.Length;i++){
            sliders[i].value = sliders[i].minValue;
        }
    }
    public void resetComboText(){
        tm.text = "";
        multiplier.text ="";
        foreach(Slider s in sliders){
            s.value = s.minValue;
        }
    }
    IEnumerator increaseSize(TMP_Text text, float amount, float time){
        text.fontSize += amount;
        yield return new WaitForSeconds(time);
        text.fontSize -=amount;
    }
}
