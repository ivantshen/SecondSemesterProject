using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[6,5];
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

        //Amount allowed
        shopItems[4,1] = 4;
        shopItems[4,2] = 3;
        shopItems[4,3] = 2;
        shopItems[4,4] = 1;
    }

    public void Buy(){
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        coins = CurrencyManager.keyy.getMon();
        //shows if u have enough money and the item is still in stock
        if(coins >= shopItems[2, ButtonRef.GetComponent<buttonInfo>().ItemID] && shopItems[4, ButtonRef.GetComponent<buttonInfo>().ItemID] != 0){
            

            coins -= shopItems[2, ButtonRef.GetComponent<buttonInfo>().ItemID];


            shopItems[3, ButtonRef.GetComponent<buttonInfo>().ItemID]++;
            CoinsTXT.text = "Coins:" + coins.ToString();
            CurrencyManager.keyy.setMon(coins);
            ButtonRef.GetComponent<buttonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<buttonInfo>().ItemID].ToString();
            Debug.Log("Works?");
            shopItems[4, ButtonRef.GetComponent<buttonInfo>().ItemID]--;
            }
        else{
            Debug.Log("Doesn't work" + coins);
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
