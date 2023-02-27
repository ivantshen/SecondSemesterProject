using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5,5];
    public int coins;
    public TextMeshProUGUI CoinsTXT;

    
    
    void Start()
    {
        coins = CurrencyManager.keyy.getMon();
        CoinsTXT.text = "Coins:" + coins.ToString();

        //IDs
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        
        //Price
        shopItems[2,1] = 10;
        shopItems[2,2] = 25;
        shopItems[2,3] = 32;
        shopItems[2,4] = 47;

        //Quantity
        shopItems[3,1] = 0;
        shopItems[3,2] = 0;
        shopItems[3,3] = 0;
        shopItems[3,4] = 0;
    }

    public void Buy(){
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if(coins >= shopItems[2, ButtonRef.GetComponent<buttonInfo>().ItemID]){

            coins -= shopItems[2, ButtonRef.GetComponent<buttonInfo>().ItemID];


            shopItems[3, ButtonRef.GetComponent<buttonInfo>().ItemID]++;
            CoinsTXT.text = "Coins:" + coins.ToString();
            CurrencyManager.keyy.setMon(coins);
            ButtonRef.GetComponent<buttonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<buttonInfo>().ItemID].ToString();
            Debug.Log("Works?");
                    }
        else{
            Debug.Log("Doesn't work");
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
