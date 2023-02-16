using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public TextMeshProUGUI currentXPtext, targetXPtext, levelText;
    public int currentXP, targetXP, level;

    public static XPManager instance;

    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        targetXPtext.text = targetXP.ToString();
        currentXPtext.text = currentXP.ToString();
        levelText.text = level.ToString();

    }


    public void AddXP(int xp){
        currentXP +=xp;

        if(currentXP >= targetXP){
            currentXP = currentXP - targetXP;
            level++;
            

            targetXP += targetXP / 20;

            levelText.text = level.ToString();
            targetXPtext.text = targetXP.ToString();
        }
        currentXPtext.text = currentXP.ToString();
    }
}
