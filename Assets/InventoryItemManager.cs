using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItemManager : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public string type;
    public GameObject attachedObject;
    private bool mouseDown = false;
    private GameObject[] inventorySlots;
    private int itemSlotIndex = -1;
    private InventoryManager inventory;
    private bool inUse = false;
    private WeaponManager wepManage;
    private GameObject instantiatedObject;
    void Start(){
        this.gameObject.GetComponent<Image>().color = Random.ColorHSV();
        inventorySlots = GameObject.FindGameObjectsWithTag("InventorySlot");
        inventory = GameObject.FindWithTag("InventorySystem").GetComponent<InventoryManager>();
        wepManage = GameObject.FindWithTag("Player").GetComponent<WeaponManager>();
        GameObject[] storedItems = inventory.getStoredItems();
        for(int i= 0;i<storedItems.Length;i++){
            if(storedItems[i]==null){
                bool pass = inventory.storeItem(i,gameObject,itemSlotIndex);
                if(pass){
                itemSlotIndex = i;
                 return;   
                }
                
            }
        }
    }
    void Update(){
        if(mouseDown){
            transform.position = Input.mousePosition;
        }
    }
    public void randomizeColor(){
        this.gameObject.GetComponent<Image>().color = Random.ColorHSV();
    }
    public void OnPointerDown(PointerEventData eventData){
            mouseDown = true;
    }
    public void setItemIndex(int index){
        itemSlotIndex = index;
        if(inUse){
        inUse = false;
        Destroy(instantiatedObject);
        }
    }
    public void setType(string type){
        this.type = type;
    }
    public void setAttachedObject(GameObject attachedObject){
        this.attachedObject = attachedObject;
    }
    public void OnPointerUp(PointerEventData eventData){
            mouseDown = false;
            Transform closestSlot = getClosestSlot().transform;
            int k = getClosestSlotIndex();
            if(Vector3.Distance(closestSlot.position,transform.position)<30){
                if(inUse){
                    inUse = false;
                    Destroy(instantiatedObject);
                    instantiatedObject = null;
                }
                inventory.storeItem(k,gameObject,itemSlotIndex);
            }else{
                    transform.position = inventorySlots[itemSlotIndex].transform.position;
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
    public string GetEquipType(){
        return type;
    }
    public GameObject getGameObject(){
        return attachedObject;
    }
    public IEnumerator convertToGameObject(){
        yield return new WaitForSeconds(0.05f);
        if(!inUse){
        instantiatedObject = Instantiate(attachedObject,transform.position,Quaternion.identity,null);
        if(!instantiatedObject.activeSelf){
            instantiatedObject.gameObject.SetActive(true);
        }
        if(type =="Weapon"){
         wepManage.initialPickUp(instantiatedObject);      
        }
        inUse = true; 
        }
    }
}
