using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasKeybinds : MonoBehaviour
{
    public string inventoryKeybind;
    public GameObject inventory;
    private bool inventoryClosed = false; 
    void Update()
    {
        if(Input.GetKeyDown(inventoryKeybind)){
            inventory.SetActive(inventoryClosed);
            inventoryClosed = !inventoryClosed;
        }
    }
}
