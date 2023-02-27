using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class buttonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceTxt;
    public TextMeshProUGUI QuantityTxt;
    public TextMeshProUGUI AmountTxt;
    public GameObject ShopManager;

    void Update(){
        PriceTxt.text = "Price: $" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2,ItemID].ToString();
        QuantityTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3,ItemID].ToString();
        AmountTxt.text = "Amount left: " + ShopManager.GetComponent<ShopManagerScript>().shopItems[4,ItemID].ToString();
        
    }
}
