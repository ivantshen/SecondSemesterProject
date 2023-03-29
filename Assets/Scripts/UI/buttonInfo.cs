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
    public ShopManagerScript ShopManager;
    void Start(){
        ShopManager = GameObject.FindWithTag("ShopManager").GetComponent<ShopManagerScript>();
    }
    void Update(){
        PriceTxt.text = "Price: $" + ShopManager.shopItems[2,ItemID].ToString();
        QuantityTxt.text = ShopManager.shopItems[3,ItemID].ToString();
        AmountTxt.text = "Amount left: " + ShopManager.shopItems[4,ItemID].ToString();
    }
}
