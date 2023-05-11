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
    [SerializeField] private LineRenderer[] lines;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private int extraArrows;
    private bool allowFire = true;
    private bool allowArrowLoad = true;
    private bool keyHeld = false;
    private float chargeValue = 0;
    private float reloadCD = 0;
    private float fireAngle = 0;
    private GameObject currentArrow;
    private Transform player;
    private Vector2 centerPointOriginalPos;
    void Start(){
        centerPointOriginalPos = new Vector2(0,0.075f);
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
        transform.localEulerAngles = new Vector3(0,player.localEulerAngles.y,transform.localEulerAngles.z);
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
            if(allowArrowLoad){
                allowArrowLoad = false;
                currentArrow = Instantiate(arrow,firePoint.position,Quaternion.Euler(0,0,fireAngle),centerPoint);
                currentArrow.transform.localPosition = new Vector2(0,1f);
            }
            if(chargeValue<1){
            chargeValue+= Time.deltaTime/chargeTime;
            centerPoint.localPosition = new Vector2(0,(-0.3f*chargeValue));
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
        }else{
            centerPoint.localPosition = Vector2.MoveTowards(centerPoint.localPosition,centerPointOriginalPos,15*Time.deltaTime);
        }
        
    }
    private void fire(){
        if(extraArrows<=0){
        currentArrow.GetComponent<Arrow>().assignStatsAndFire(chargeValue,(currentArrow.transform.position-player.position).normalized);    
        }else{
        for(int i=0;i<=extraArrows;i++){
            GameObject ar = currentArrow.transform.GetChild(0).gameObject;
            ar.GetComponent<Arrow>().assignStatsAndFire(chargeValue,(ar.transform.position-player.position).normalized);
            ar.transform.parent = null;
        }
        }
        allowArrowLoad = true;
        chargeValue=0;
    }
}
