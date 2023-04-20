using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CanvasKeybinds : MonoBehaviour
{   
    [SerializeField] private PlayerInput inputManager;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject hotkeyMenu;

    private bool inventoryClosed = false; 
    private bool hotkeyMenuClosed = false;

    public void openInventory(InputAction.CallbackContext context){
        if(context.performed){
            inventory.SetActive(inventoryClosed);
            inventoryClosed = !inventoryClosed;    
        }
    }
    public GameObject getInventoryGameObject(){
        return inventory;
    }

    public void openHotkeyMenu(InputAction.CallbackContext context){
        if(context.performed){
            hotkeyMenu.SetActive(hotkeyMenuClosed);
            hotkeyMenuClosed = !hotkeyMenuClosed;    

            
            inputManager.actions.Disable();
            if (hotkeyMenuClosed) {
                inputManager.actions.Enable();
            } 
        }   
    }

    public GameObject getHotkeyMenuGameObject(){
        return hotkeyMenu;
    }

    
}
