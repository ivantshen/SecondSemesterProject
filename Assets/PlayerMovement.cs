using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool grounded= false;
    private bool ableToWalk = true;
    private float jumpCharge = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ableToWalk){
            rb.velocity = new Vector2(Input.GetAxis("Horizontal"),rb.velocity.y);
        }
    }

    private void OnCollision2D(Collision2D other){
        if(other.gameObject.tag=="Ground"){
            grounded = true;
        }
    }
}
