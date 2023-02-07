using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string equipDropKey;
    public string switchKey;
    public float switchCooldown;
    public float pickupRange;
    private bool allowSwitch = true;
    private float switchTimer;
    private LayerMask weaponLayer;
    private GameObject equipped = null;
    private GameObject stored = null;
    void Start(){
        weaponLayer = LayerMask.GetMask("Weapon");
    }
    void Update(){
        if(switchTimer>0){
            switchTimer-=Time.deltaTime;
        }else{
            allowSwitch = true;
        }
        if(Input.GetKeyDown(switchKey)&&allowSwitch){
            allowSwitch = false;
            switchWeapon();
            switchTimer = switchCooldown;
        }
        if(Input.GetKeyDown(equipDropKey)){
        Collider2D[] weaponsToPickUp = Physics2D.OverlapCircleAll(transform.position,pickupRange,weaponLayer);
         foreach(Collider2D weapon in weaponsToPickUp){
             initialPickUp(weapon.gameObject);
         }    
        }
    }
    private void switchWeapon (){
            GameObject tempEquipped = equipped;
            GameObject tempStored = stored;
            equipWeapon(tempStored);
            storeWeapon(tempEquipped);
    }
    public void storeWeapon (GameObject weapon){
        stored = weapon;
        if(weapon){
        if(transform.rotation==Quaternion.identity){
        stored.transform.rotation = Quaternion.identity;    
        }else{
        stored.transform.rotation = Quaternion.Euler(0,180,0);
        }
        activateScripts(stored,false);
        stored.SendMessage("SetOrientationStored");    
        }
    }
    public void initialPickUp(GameObject weapon){
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
        }else{
            //put in inventory
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
        activateScripts(equipped,true);
        equipped.SendMessage("SetOrientationOriginal");     
        }
        
    }
    private static void activateScripts(GameObject weapon, bool TF){
        if(weapon.GetComponent<MonoBehaviour>()!=null){
            weapon.GetComponent<MonoBehaviour>().enabled = TF;
        }
    }
    void OnDrawGizmosSelected() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,pickupRange);
    }
}
