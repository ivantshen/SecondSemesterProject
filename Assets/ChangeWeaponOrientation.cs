using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponOrientation : MonoBehaviour
{
    public float storedXPos;
    public float storedYPos;
    public float storedZPos;
    public float storedZRot;
    public Transform spriteToManipulate;

    public Vector3 originalPos;
    public float originZRot;
    public void SetOrientationStored(){
            spriteToManipulate.localPosition = new Vector3(storedXPos,storedYPos,storedZPos);
            if(transform.rotation==Quaternion.identity){
            spriteToManipulate.rotation = Quaternion.Euler(0,0,storedZRot);
            }else{
            spriteToManipulate.rotation = Quaternion.Euler(0,180,storedZRot);
            }
            
    }
    public void SetOrientationOriginal(){
        spriteToManipulate.localPosition = originalPos;
        if(transform.rotation==Quaternion.identity){
            spriteToManipulate.rotation = Quaternion.Euler(0,0,originZRot);
            }else{
            spriteToManipulate.rotation = Quaternion.Euler(0,180,originZRot);
    }
}
}
