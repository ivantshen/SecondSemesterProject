using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string equipDropKey;
    public string switchKey;
    public float switchCooldown;
    private bool allowSwitch = true;
    private float switchTimer;
    private GameObject equipped = null;
    private GameObject stored = null;

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
    }
    private void switchWeapon (){
            GameObject temp = equipped;
            if(stored&&!equipped){
            equipWeapon(stored);
            equipped.SendMessage("ChangeOrientation");    
            stored = null;
            Debug.Log("stored&&!equipped");
            }
            if(equipped&&!stored){
            storeWeapon(temp);    
            equipped = null;
            Debug.Log("equipped&&!stored");
            }
            if(equipped&&stored){
            equipWeapon(stored);
            equipped.SendMessage("ChangeOrientation");    
            storeWeapon(temp); 
            Debug.Log("SWITCH");
            }
            
    }
    public void storeWeapon (GameObject weapon){
        weapon.SendMessage("ChangeOrientation");
        activateScripts(weapon,false);
        stored = weapon;
    }
    public void equipWeapon (GameObject weapon){
        if(weapon.GetComponent<Rigidbody2D>()){
        Destroy(weapon.GetComponent<Rigidbody2D>());    
        }
        weapon.GetComponent<Collider2D>().enabled = false;
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0,0,0);
        if(transform.rotation==Quaternion.identity){
        weapon.transform.rotation = Quaternion.identity;    
        }else{
        weapon.transform.rotation = Quaternion.Euler(0,180,0);
        }

        activateScripts(weapon,true);
        equipped = weapon;
    }
    private static void activateScripts(GameObject weapon, bool TF){
        if(weapon.GetComponent<MonoBehaviour>()!=null){
            weapon.GetComponent<MonoBehaviour>().enabled = TF;
        }
    }
    private void OnTriggerStay2D(Collider2D other){
        if(Input.GetKey(equipDropKey)&&other.tag=="Weapon"){
            equipWeapon(other.gameObject);
        }
    }
}
