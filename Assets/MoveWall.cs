using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWall : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject boss;
    public GameObject player;
    private CapsuleCollider2D cd;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            rb.gravityScale = 10;
            boss.SendMessage("BossTransformation");
            Destroy(cd); // turn off the collider
            player.SendMessage("FreezePosition", 5f);
        }
        
    }
}
