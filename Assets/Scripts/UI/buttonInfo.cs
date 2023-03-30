using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class buttonInfo : MonoBehaviour
{
    public int ItemID;
    public TextMeshProUGUI PriceTxt;
    public TextMeshProUGUI QuantityTxt;
    public TextMeshProUGUI AmountTxt;
    public ShopManagerScript ShopManager;
    private Button thisButton;
    void Awake(){
        thisButton = gameObject.GetComponent<Button>();
        ShopManager = ShopManagerScript.sm[SceneManager.GetActiveScene().buildIndex];
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        thisButton.onClick.AddListener(ShopManager.Buy);
    }
    
    void Update(){
        if(!ShopManager){
            ShopManager = ShopManagerScript.sm[SceneManager.GetActiveScene().buildIndex];
        }
        PriceTxt.text = "Price: $" + ShopManager.shopItems[2,ItemID].ToString();
        QuantityTxt.text = ShopManager.shopItems[3,ItemID].ToString();
        AmountTxt.text = "Amount left: " + ShopManager.shopItems[4,ItemID].ToString();
    }
}
