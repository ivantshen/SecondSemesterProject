using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public string equipDropKey;
    private GameObject equipped;
    private GameObject stored;

    public void equipWeapon (GameObject weapon){
        Destroy(weapon.GetComponent<Rigidbody2D>());
        weapon.GetComponent<Collider2D>().enabled = false;
        weapon.transform.SetParent(transform);
        weapon.transform.localPosition = new Vector3(0,0,0);
        weapon.transform.rotation = Quaternion.identity;
        activateScripts(weapon);
        equipped = weapon;
    }
    private static void activateScripts(GameObject weapon){
        if(weapon.GetComponent<MonoBehaviour>()!=null){
            weapon.GetComponent<MonoBehaviour>().enabled = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other){
        if(Input.GetKey(equipDropKey)&&other.tag=="Weapon"){
            equipWeapon(other.gameObject);
        }
    }
}
