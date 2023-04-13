using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponOrientation : MonoBehaviour
{
    public Transform spriteToManipulate;
    public SpriteRenderer[] sprite;
    public Vector3 storedPos;
    public float storedZRot;
    public int storedSortingOrder;
    public Vector3 originalPos;
    public float originZRot;
    public int originSortingOrder;
    public void SetOrientationStored(){
            spriteToManipulate.localPosition = storedPos;
            foreach(SpriteRenderer s in sprite){
            s.sortingOrder = storedSortingOrder;    
            }
            if(transform.rotation==Quaternion.identity){
            spriteToManipulate.rotation = Quaternion.Euler(0,0,storedZRot);
            }else{
            spriteToManipulate.rotation = Quaternion.Euler(0,180,storedZRot);
            }
            
    }
    public void SetOrientationOriginal(){
        spriteToManipulate.localPosition = originalPos;
        foreach(SpriteRenderer s in sprite){
            s.sortingOrder = originSortingOrder;    
            }
        if(transform.rotation==Quaternion.identity){
            spriteToManipulate.rotation = Quaternion.Euler(0,0,originZRot);
            }else{
            spriteToManipulate.rotation = Quaternion.Euler(0,180,originZRot);
    }
}
}
