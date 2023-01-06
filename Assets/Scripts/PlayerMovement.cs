using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    [SerializeField] private float speed; // [SerializeField lets you edit in Unity]
    
    private bool isGrounded;
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public float canJumpTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // GetComponent looks for the component in the inspector
        isGrounded = true;
        
    }

    void FixedUpdate() {
        // Vector2 for 2D movement; Input.GetAxis("Horizontal") gives -1 or 1 
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y); 
        if (Input.GetAxis("Horizontal") > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (Input.GetAxis("Horizontal") < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded) {
            canJumpTime = 0.15f;
        }
        canJumpTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && canJumpTime > 0) {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true) {
            rb.velocity = Vector2.up * jumpForce;
            if (jumpTimeCounter > 0) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        // if player stops holding down space they aren't jumping anymore
        if (Input.GetKeyUp(KeyCode.Space)) {
            isJumping = false;
            canJumpTime = 0;
            StartCoroutine(gravityChange());
        }
    }

    IEnumerator gravityChange() {
        rb.gravityScale = 2;
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = 4;
    } 

    
}
