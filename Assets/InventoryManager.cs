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
    private bool full = false;
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
        bool movingFromEquipment = false;
        for(int i=0;i<equipmentSlotIndexes.Length;i++){
            if(index == equipmentSlotIndexes[i]){
                accessingEquipment = true;
            }
            if(previousItemIndex == equipmentSlotIndexes[i]){
                movingFromEquipment = true;
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
                storedItems[previousItemIndex].GetComponent<InventoryItemManager>().setItemIndex(previousItemIndex);
                if(movingFromEquipment){
                 StartCoroutine(storedItems[previousItemIndex].GetComponent<InventoryItemManager>().convertToGameObject());   
                }
            }
            item.GetComponent<InventoryItemManager>().setItemIndex(index);
            StartCoroutine(item.GetComponent<InventoryItemManager>().convertToGameObject());
            checkIfFull();
            return true;
            }else{
                item.transform.position = inventorySlots[previousItemIndex].transform.position;
            return false;    
            }
        }else{
            if(movingFromEquipment&&storedItems[index]){
                bool passCheck = false;
                for(int i=0;i<equipmentSlots.Length;i++){
                if(equipmentSlots[i]==inventorySlots[previousItemIndex]){
                    if(equipmentSlotType[i]==storedItems[index].GetComponent<InventoryItemManager>().GetEquipType()){
                        passCheck = true;
                    }
                }
            }
            if(passCheck){
            GameObject tempStored = storedItems[index];
            storedItems[index] = item;
            storedItems[previousItemIndex]  = tempStored;
            storedItems[previousItemIndex].transform.position = inventorySlots[previousItemIndex].transform.position;
            storedItems[previousItemIndex].GetComponent<InventoryItemManager>().setItemIndex(previousItemIndex);
            StartCoroutine(storedItems[previousItemIndex].GetComponent<InventoryItemManager>().convertToGameObject());
            item.GetComponent<InventoryItemManager>().setItemIndex(index);
        storedItems[index].transform.position = inventorySlots[index].transform.position;  
        checkIfFull();
                return true;
            }else{
                StartCoroutine(item.GetComponent<InventoryItemManager>().convertToGameObject());
                item.transform.position = inventorySlots[previousItemIndex].transform.position;
                return false;
            }
            }else {
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
            storedItems[previousItemIndex].GetComponent<InventoryItemManager>().setItemIndex(previousItemIndex);
        }
        item.GetComponent<InventoryItemManager>().setItemIndex(index);
        storedItems[index].transform.position = inventorySlots[index].transform.position;  
        checkIfFull();
        return true;      
            }
        }
    }
    private void checkIfFull(){
    bool check = true;
        for(int i=0;i<storedItems.Length;i++){
            if(!storedItems[i]){
                check = false;
            }
        }
        full = check;
    }
    public bool getFullStatus(){
        return full;
    }
    public GameObject[] getStoredItems(){
        return storedItems;
    }
}
