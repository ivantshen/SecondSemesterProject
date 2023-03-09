using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashRefill : MonoBehaviour
{   
    public Rigidbody2D rb;
    public GameObject player;
    private Collider2D cd;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            Destroy(cd); // turn off the collider
            player.SendMessage("DashRefill");
        }
        
    }
}
