using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CanvasKeybinds : MonoBehaviour
{   
    [SerializeField] private PlayerInput inputManager;
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject hotkeyMenu;
    [SerializeField] private GameObject leaderboard;
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
            leaderboard.SetActive(leaderboardClosed);
            leaderboardClosed = !leaderboardClosed;    
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
    public void setHotkeyMenuState(bool tOrF){
        hotkeyMenu.SetActive(tOrF);
        hotkeyMenuClosed = !tOrF;
    }
    public GameObject getHotkeyMenuGameObject(){
        return hotkeyMenu;
    }

    
}
