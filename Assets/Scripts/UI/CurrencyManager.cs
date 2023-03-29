using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public TextMeshProUGUI currencyText;
    [SerializeField] private int currency;

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

    public int getMon(){
        return currency;
    }

    public void setMon(int monAmt){
        currency = monAmt;
    }

    public void AddMon(int monAmt){
        currency += monAmt;
        

       
        currencyText.text = currency.ToString();
    }
}
