using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float chargeTime;
    [SerializeField] private float reloadTime;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float maxRotation;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private string rotateLeftKey;
    [SerializeField] private string rotateRightKey;
    private bool allowFire = true;
    private bool keyHeld = false;
    private float chargeValue = 0;
    private float reloadCD = 0;
    private float fireAngle = 0;
    private bool flipped = false;
    private Transform player;
    void Start(){
        player = PlayerPersistence.Instance.transform;
    }
    // Update is called once per frame
    public void onHold(InputAction.CallbackContext context)
    {
        keyHeld = context.ReadValueAsButton();
    }
    void Update(){
        fireAngle = transform.eulerAngles.z;
        if(fireAngle>180){
            fireAngle = fireAngle-360;
        }
        transform.eulerAngles = new Vector3(0,player.eulerAngles.z,transform.eulerAngles.z);
        if(Input.GetKey(rotateLeftKey)&&fireAngle<maxRotation){
            transform.RotateAround(player.position,Vector3.forward,rotateSpeed*Time.deltaTime);
        }else if(Input.GetKey(rotateRightKey)&&fireAngle>-maxRotation){
            transform.RotateAround(player.position,Vector3.forward,-rotateSpeed*Time.deltaTime);
        }
        if(reloadCD>0){
            reloadCD-=Time.deltaTime;
        }else{
            allowFire=true;
        }
        if(allowFire){
        if(keyHeld){
            if(chargeValue<1){
            chargeValue+= Time.deltaTime/chargeTime;    
            }else{
            chargeValue = 1;
            }
        }else{
            if(chargeValue>0){
                reloadCD = reloadTime;
                allowFire=false;
                fire();
            }
        }
        }
        
    }
    private void fire(){
        GameObject a = Instantiate(arrow,firePoint.position,Quaternion.Euler(0,0,fireAngle),null);

        chargeValue=0;
    }
}
