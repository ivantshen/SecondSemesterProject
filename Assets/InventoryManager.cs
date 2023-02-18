using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameObject[] storedItems;
    private GameObject[] inventorySlots;
    void Start()
    {
        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
       storedItems = new GameObject[GameObject.FindGameObjectsWithTag("InventorySlot").Length];
    }
    public void storeItem(int index, GameObject item, int previousItemIndex){
        if(!storedItems[index]){
            if(previousItemIndex>-1){
            storedItems[previousItemIndex] = null;    
            }
            storedItems[index] = item;
        }else{
            GameObject tempStored = storedItems[index];
            storedItems[index] = item;
            storedItems[previousItemIndex]  = tempStored;
            storedItems[previousItemIndex].transform.position = inventorySlots[previousItemIndex].transform.position;
        }
        storedItems[index].transform.position = inventorySlots[index].transform.position;
    }
    public GameObject[] getStoredItems(){
        return storedItems;
    }
}
