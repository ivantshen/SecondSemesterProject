using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemManager : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private bool mouseDown = false;
    private GameObject[] inventorySlots;
    private int itemSlotIndex = -1;
    private InventoryManager inventory;

    void Start(){
        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        inventory = GameObject.FindWithTag("InventorySystem").GetComponent<InventoryManager>();
        GameObject[] storedItems = inventory.getStoredItems();
        for(int i= 0;i<storedItems.Length;i++){
            if(storedItems[i]==null){
                inventory.storeItem(i,gameObject,itemSlotIndex);
                itemSlotIndex = i;
                return;
            }
        }
    }
    void Update(){
        if(mouseDown){
            transform.position = Input.mousePosition;
        }
    }
    public void OnPointerDown(PointerEventData eventData){
            mouseDown = true;
    }
    public void OnPointerUp(PointerEventData eventData){
            mouseDown = false;
            Transform closestSlot = getClosestSlot().transform;
            if(Vector3.Distance(closestSlot.position,transform.position)<20){
                inventory.storeItem(getClosestSlotIndex(),gameObject,itemSlotIndex);
                itemSlotIndex = getClosestSlotIndex();
            }
    }
    private GameObject getClosestSlot(){
        float minDistance = 20000;
        GameObject closest = null;
        foreach(GameObject slot in inventorySlots){
            float distance = Vector3.Distance(slot.transform.position,transform.position);
            if(distance<minDistance){
                minDistance = distance;
                closest = slot;
            }
        }
        return closest;
    }
    private int getClosestSlotIndex(){
        float minDistance = 20000;
        int closest = 0;
        for(int i=0;i<inventorySlots.Length;i++){
            float distance = Vector3.Distance(inventorySlots[i].transform.position,transform.position);
            if(distance<minDistance){
                minDistance = distance;
                closest = i;
            }
        }
        return closest;
    }
}
