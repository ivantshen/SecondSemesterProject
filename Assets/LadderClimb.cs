using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float vertical;
    private float speed = 8f;
    private bool onLadder;
    private bool isClimbing;

    
    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (onLadder && Mathf.Abs(vertical) > 0f) {
            isClimbing = true;
        }
    }

    private void FixedUpdate() {
        if (isClimbing) {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        } else {
            rb.gravityScale = 7f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Ladder")) {
            onLadder = false;
            isClimbing = false;
        }
    }
}
