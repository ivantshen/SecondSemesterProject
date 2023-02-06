using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponOrientation : MonoBehaviour
{
    public float xPos;
    public float yPos;
    public float zPos;
    public float zRot;
    public Transform spriteToManipulate;
    private bool changed = false;

    private Vector3 originalPos;
    private float originZRot;
    void Start(){
    originalPos = spriteToManipulate.position;
    originZRot = spriteToManipulate.rotation.z;
    }
    public void ChangeOrientation(){
        if(changed == false){
            spriteToManipulate.localPosition = new Vector3(xPos,yPos,zPos);
            spriteToManipulate.rotation = Quaternion.Euler(0,0,zRot);
        }else{
            spriteToManipulate.localPosition = originalPos;
            spriteToManipulate.rotation = Quaternion.Euler(0,0,originZRot);
        }
        changed = !changed;
    }
}
