using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemManager : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private bool mouseDown = false;
    private GameObject[] inventorySlots;
    void Start(){
        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
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
                transform.position = closestSlot.position;
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
}
