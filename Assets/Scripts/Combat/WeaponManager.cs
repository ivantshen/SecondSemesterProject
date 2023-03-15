using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    public float switchCooldown;
    public float pickupRange;
    private bool allowSwitch = true;
    private float switchTimer;
    private LayerMask weaponLayer;
    private GameObject equipped = null;
    private GameObject stored = null;
    private GameObject inventory;
    public GameObject inventoryItemTemplate;
    void Start(){
        Physics2D.IgnoreLayerCollision(3,3,true);
        Physics2D.IgnoreLayerCollision(3,7,true);
        weaponLayer = LayerMask.GetMask("Weapon");
        inventory = GameObject.FindWithTag("Canvas").GetComponent<CanvasKeybinds>().getInventoryGameObject();
    }
    void Update(){
        if(switchTimer>0){
            switchTimer-=Time.deltaTime;
        }else{
            allowSwitch = true;
        }
    }
    public void pickUp(InputAction.CallbackContext context){
        Collider2D[] weaponsToPickUp = Physics2D.OverlapCircleAll(transform.position,pickupRange,weaponLayer);
        foreach(Collider2D weapon in weaponsToPickUp){
        putInInventory(weapon.gameObject);
        } 
    }
    public void switchWeapon(InputAction.CallbackContext context){
        if(allowSwitch){
            allowSwitch = false;
            GameObject tempEquipped = equipped;
            GameObject tempStored = stored;
            equipWeapon(tempStored);
            storeWeapon(tempEquipped);   
            switchTimer = switchCooldown;
        }
            
    }
    public void storeWeapon (GameObject weapon){
        stored = weapon;
        if(weapon){
        if(transform.rotation==Quaternion.identity){
        stored.transform.rotation = Quaternion.identity;    
        }else{
        stored.transform.rotation = Quaternion.Euler(0,180,0);
        }
        activateWeapon(stored,false);
        stored.SendMessage("SetOrientationStored");    
        }
    }
    public void initialPickUp(GameObject weapon){
        if(!equipped||!stored){
        if(weapon.GetComponent<Rigidbody2D>()){
        Destroy(weapon.GetComponent<Rigidbody2D>());    
        }
        weapon.GetComponent<Collider2D>().enabled = false;
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0,0,0);
        if(!equipped){
            equipWeapon(weapon);
        }else if(!stored){
            storeWeapon(weapon);
        }    
        }
    }
    public void putInInventory(GameObject weapon){
        if(!inventory.GetComponent<InventoryManager>().getFullStatus()){
            GameObject inventoryItem = Instantiate(inventoryItemTemplate,transform.position,Quaternion.identity,inventory.transform);
            InventoryItemManager itemManager =inventoryItem.GetComponent<InventoryItemManager>();
            itemManager.setType("Weapon");
            itemManager.setAttachedObject(weapon);
            itemManager.randomizeColor();
            weapon.SetActive(false);  
            }
    }
    public void equipWeapon (GameObject weapon){
        equipped = weapon;
        if(equipped){
        if(transform.rotation==Quaternion.identity){
        equipped.transform.rotation = Quaternion.identity;    
        }else{
        equipped.transform.rotation = Quaternion.Euler(0,180,0);
        }
        activateWeapon(equipped,true);
        equipped.SendMessage("SetOrientationOriginal");     
        }
        
    }
    private static void activateWeapon(GameObject weapon, bool TF){
        MonoBehaviour[] scripts = weapon.GetComponentsInChildren<MonoBehaviour>();
        foreach(MonoBehaviour s in scripts){
        if(s!=null){
            s.enabled = TF;
        }    
        }
        if(weapon.GetComponent<PlayerInput>()){
        weapon.GetComponent<PlayerInput>().enabled = TF;    
        }
        
        
    }
    public void findAndUnequip(GameObject weapon){
        if(weapon==equipped){
            equipped=null;
        }
        if(weapon==stored){
            stored=null;
        }
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,pickupRange);
    }
}
