using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    public int currency;

    public static CurrencyManager keyy;

    private void Awake(){
        if(keyy == null){
            keyy = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        currencyText.text = currency.ToString();
        

    }


    public void AddXP(int monAmt){
        currency += monAmt;
        

       
        currencyText.text = currency.ToString();
    }
}
