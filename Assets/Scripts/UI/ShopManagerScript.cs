using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[6,5];
    public int coins;
    public TextMeshProUGUI CoinsTXT;
    [SerializeField] private GameObject[] prefab;
    //[SerializeField] private Vector2 spawnPosition;
    //[SerializeField] private bool random;
    [SerializeField] private float xPos;
    [SerializeField] private float yPos;
    private int sceneIndex;
    private Transform player;
    public static ShopManagerScript[] sm;
    void Awake(){
        if(sm==null||sm.Length==0){
            sm = new ShopManagerScript[SceneManager.sceneCountInBuildSettings];
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        for(int i=0;i<sm.Length;i++){
            if(sm[i]&&sceneIndex==i){
                Destroy(this.gameObject);
                return;
            }
        }
        sm[sceneIndex] = this;
        DontDestroyOnLoad(this);
    }
    
    void Start()
    {
        player = PlayerPersistence.Instance.transform;
        CoinsTXT = CanvasPersistence.Instance.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>();
        coins = CurrencyManager.keyy.getMon();
        CoinsTXT.text = "Coins:" + coins.ToString();

        //IDs
        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;
        
        //Price
        shopItems[2,1] = 100;
        shopItems[2,2] = 25;
        shopItems[2,3] = 32;
        shopItems[2,4] = 500;

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
            shopItems[4, ButtonRef.GetComponent<buttonInfo>().ItemID]--;



            if(shopItems[1, ButtonRef.GetComponent<buttonInfo>().ItemID] == 1){
               OnSpawnPrefab(1);

            }
            else if(shopItems[1, ButtonRef.GetComponent<buttonInfo>().ItemID] == 2){
                OnSpawnPrefab(2);
            }
            else if(shopItems[1, ButtonRef.GetComponent<buttonInfo>().ItemID] == 3){
                OnSpawnPrefab(3);
            }
            else if(shopItems[1, ButtonRef.GetComponent<buttonInfo>().ItemID] == 4){
                OnSpawnPrefab(4);
            }
            }
        else{
        }


    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnSpawnPrefab(int ID){
        if(ID == 1){
        //Instantiate(prefab1, new Vector2(xPos,yPos), Quaternion.identity);
            player.gameObject.GetComponent<HealthP1>().TakeDamage(-50);
        }
        else if(ID == 2){
            Instantiate(prefab[1], new Vector2(xPos,yPos), Quaternion.identity);
        }
        else if(ID == 3){
            Instantiate(prefab[2], new Vector2(xPos,yPos), Quaternion.identity);
        }
        else if(ID == 4){
            Instantiate(prefab[3], new Vector2(xPos,yPos), Quaternion.identity);

        }
    }
}
