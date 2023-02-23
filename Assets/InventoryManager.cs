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
        Transform[] transformArray = GameObject.FindWithTag("EquipmentSlots").GetComponentsInChildren<Transform>();
        equipmentSlots = new GameObject[transformArray.Length-1];
        for (int i =1;i<transformArray.Length;i++){
            equipmentSlots[i-1] = transformArray[i].gameObject;
        }
        storedEquipment = new GameObject[equipmentSlots.Length];
        equipmentSlotIndexes = new int[equipmentSlots.Length];
        for(int i=0;i<equipmentSlots.Length;i++){
            for(int k=0;k<inventorySlots.Length;k++){
                if (equipmentSlots[i]==inventorySlots[k]){
                    equipmentSlotIndexes[i] = k;
                }
            }
        }
    }
    public bool storeItem(int index, GameObject item, int previousItemIndex){
        bool accessingEquipment = false;
        for(int i=0;i<equipmentSlotIndexes.Length;i++){
            if(index == equipmentSlotIndexes[i]){
                accessingEquipment = true;
            }
        }
        if(accessingEquipment){
            bool passCheck = false;
            for(int i=0;i<equipmentSlots.Length;i++){
                if(equipmentSlots[i]==inventorySlots[index]){
                    if(equipmentSlotType[i]==item.GetComponent<InventoryItemManager>().GetEquipType()){
                        passCheck = true;
                    }
                }
            }
            if(passCheck){
            if(!storedItems[index]){
                if(previousItemIndex>-1){
                storedItems[previousItemIndex] = null;    
                }
                storedItems[index] = item;
                storedItems[index].transform.position = inventorySlots[index].transform.position;  
            }else{
                GameObject tempStored = storedItems[index];
                storedItems[index] = item;
                storedItems[previousItemIndex]  = tempStored;
                storedItems[previousItemIndex].transform.position = inventorySlots[previousItemIndex].transform.position;
                storedItems[index].transform.position = inventorySlots[index].transform.position;  
            }    
            }else{
            return false;    
            }
        }else{
        if(!storedItems[index]){
            if(previousItemIndex>-1){
            storedItems[previousItemIndex] = null;    
            }
            storedItems[index] = item;
            return true;
        }else{
            GameObject tempStored = storedItems[index];
            storedItems[index] = item;
            storedItems[previousItemIndex]  = tempStored;
            storedItems[previousItemIndex].transform.position = inventorySlots[previousItemIndex].transform.position;
        }
        storedItems[index].transform.position = inventorySlots[index].transform.position;  
        return true;  
        }

    }
    public GameObject[] getStoredItems(){
        return storedItems;
    }
}
