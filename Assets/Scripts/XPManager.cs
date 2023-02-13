using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XPManager : MonoBehaviour
{
    public TextMeshProUGUI currentXPtext, targetXPtext;
    public int currentXP, targetXP;

    public static XPManager instance;

    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    private void AddXP(int xp){
        currentXP +=xp;
        currentXPtext.text = currentXP.ToString();
    }
}
