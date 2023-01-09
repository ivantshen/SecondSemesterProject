using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotateScript : MonoBehaviour
{
    public bool allowCardinalAiming = true;
    public bool allowDiagonalAiming = true;
    public Transform whatToRotate;
    // Update is called once per frame
    void FixedUpdate()
    {
            whatToRotate.rotation = Quaternion.Euler(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0f);
        
    }
}
