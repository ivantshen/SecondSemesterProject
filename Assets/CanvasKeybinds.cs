using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasKeybinds : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    private bool inventoryClosed = false; 
    public void openInventory(InputAction.CallbackContext context){
        if(context.performed){
        inventory.SetActive(inventoryClosed);
        inventoryClosed = !inventoryClosed;    
        }
    }
    public GameObject getInventoryGameObject(){
        return inventory;
    }
}
