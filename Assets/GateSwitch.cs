using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateSwitch : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private float gateSpeed;
    private Vector2 gateStartPosition;
    [SerializeField] private Vector2 gatePositionOffset;
    private bool raiseGate = false;
    [SerializeField] private float switchZRot;
    private float originZRot;
    // Start is called before the first frame update
    void Start()
    {
        gateStartPosition = gate.transform.position;
        originZRot = transform.rotation.z;
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
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag=="Player"){
            if(raiseGate){
                transform.rotation = Quaternion.Euler(0,0,originZRot);
                raiseGate = false;
            }else{
                transform.rotation = Quaternion.Euler(0,0,switchZRot);
                raiseGate = true;
            }
        }
    }
}
