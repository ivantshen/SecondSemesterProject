using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GateSwitch : MonoBehaviour
{
    [SerializeField] private float flickRadius;
    [SerializeField] private GameObject gate;
    [SerializeField] private float gateSpeed;
    private Vector2 gateStartPosition;
    [SerializeField] private Vector2 gatePositionOffset;
    private bool raiseGate = false;
    [SerializeField] private float switchZRot;
    [SerializeField] private GameObject text;
    [SerializeField] private Vector2 textOffset;
    private Camera cam;
    private float originZRot;
    // Start is called before the first frame update
    void Start()
    {
        gateStartPosition = gate.transform.position;
        originZRot = transform.rotation.z;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(raiseGate){
            gate.transform.position = Vector2.MoveTowards(gate.transform.position,gateStartPosition+gatePositionOffset,gateSpeed);
        }else{
            gate.transform.position = Vector2.MoveTowards(gate.transform.position,gateStartPosition,gateSpeed);
        }
    }
    public void openOrLowerGate(InputAction.CallbackContext context){
        if(context.performed&&Vector2.Distance(transform.position,PlayerPersistence.Instance.transform.position)<=flickRadius){
            if(raiseGate){
                transform.rotation = Quaternion.Euler(0,0,originZRot);
                raiseGate = false;
            }else{
                transform.rotation = Quaternion.Euler(0,0,switchZRot);
                raiseGate = true;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other){
        text.SetActive(true);
        text.transform.position = (Vector2)cam.WorldToScreenPoint(transform.position)+textOffset;
    }
    private void OnTriggerExit2D(Collider2D other){
        text.SetActive(false);
    }
}
