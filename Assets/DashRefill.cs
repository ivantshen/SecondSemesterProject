using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRefill : MonoBehaviour
{   
    public GameObject player;
    private CapsuleCollider2D cd;

    void Start() {
        cd = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(cd); // turn off the collider
            
            player.SendMessage("DashRefill");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            player.SendMessage("NotOnDash");
        }
        
    }
}
