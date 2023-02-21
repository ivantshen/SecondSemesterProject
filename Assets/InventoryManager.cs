using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameObject[] storedItems;
    private GameObject[] inventorySlots;
    private GameObject[] equipmentSlots;
    private int[] equipmentSlotIndexes;
    private GameObject[] storedEquipment;
    private string[] equipmentSlotType = {"Head","Chest","Leg","Weapon","Weapon","Charm"};
    void Start()
    {
        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        storedItems = new GameObject[inventorySlots.Length];
        equipmentSlots = GameObject.FindWithTag("EquipmentSlots").transform.GetComponentsInChildren<GameObject>();
        storedEquipment = new GameObject[equipmentSlots.Length];
        for(int i=0;i<equipmentSlots.Length;i++){
            for(int k=0;i<inventorySlots.Length;i++){
                
            }
        }
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
