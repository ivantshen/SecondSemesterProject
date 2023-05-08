using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class CanvasKeybinds : MonoBehaviour
{   
    [SerializeField] private PlayerInput inputManager;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject hotkeyMenu;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private TMP_Text text;
    private bool inventoryClosed = false; 
    private bool hotkeyMenuClosed = false;
    private bool leaderboardClosed = false;

    public void openInventory(InputAction.CallbackContext context){
        if(context.performed){
            inventory.SetActive(inventoryClosed);
            inventoryClosed = !inventoryClosed;    
        }
    }
    public GameObject getInventoryGameObject(){
        return inventory;
    }

    public void openLeaderboard(InputAction.CallbackContext context){
        if(context.performed){
            leaderboard.SetActive(inventoryClosed);
            leaderboardClosed = !inventoryClosed;    
        }
    }
    public void openHotkeyMenu(InputAction.CallbackContext context){
        if(context.performed){
            hotkeyMenu.SetActive(hotkeyMenuClosed);
            hotkeyMenuClosed = !hotkeyMenuClosed;    

            
            inputManager.actions.Disable();
            if (hotkeyMenuClosed) {
                inputManager.actions.Enable();
                Debug.Log("Enabled");
            } 
        }   
    }

    public GameObject getHotkeyMenuGameObject(){
        return hotkeyMenu;
    }

    
}
