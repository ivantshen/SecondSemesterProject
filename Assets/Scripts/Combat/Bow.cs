using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private float chargeTime;
    [SerializeField] private float reloadTime;
    private bool keyHeld = false;
    // Update is called once per frame
    void onHold(InputAction.CallbackContext context)
    {
        keyHeld = context.ReadValueAsButton();
    }
    void Update(){
        
    }
}
