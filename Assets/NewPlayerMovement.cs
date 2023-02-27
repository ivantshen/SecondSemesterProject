using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{   
    public Rigidbody2D rb;
    private TrailRenderer dashTrail;
    public Transform groundCheck;
    public LayerMask groundlayer;

    private float horzInput;
    private float vertInput;
    private float speed = 8f;
    private bool isFacingRight = true;

    // variables to control whether you can perform actions
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] public bool unlockedDash = true;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;

    // general variables
    private float playerGravity = 7.0f;

    // jump variables
    [SerializeField] private float jumpForce = 30f;
    private float jumpTimeCounter;
    private float jumpTime;
    private bool isJumping;
    private float canJumpTime;

    public float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    // dash variables
    [SerializeField] private float dashVelocity = 14f;
    [SerializeField] private float dashTime = 0.5f;
    private Vector2 dashDirection;

    // ladder climb variables
    private float fallSpeed = 8f;
    private bool onLadder;
    private bool isClimbing;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // GetComponent looks for the component in the inspector
        dashTrail = GetComponent<TrailRenderer>();
        dashTrail.emitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horzInput * speed, rb.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);

        // flip the player direction
        if (!isFacingRight && horzInput > 0f) {
            Flip();
        } else if (isFacingRight && horzInput < 0f) {
            Flip();
        }

        // is grounded checks
        if (isGrounded) {
            canJumpTime = 0.15f;
            if (unlockedDash) {
                canDash = true;
            }
        } else {
            canJumpTime -= Time.deltaTime;
        }

        // dash
        if (isDashing) {
            rb.gravityScale = 0;
            rb.velocity = dashDirection * dashVelocity;
            return;
        }

    }

    public bool isGroundedCheck() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
        
    }

    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }


    // jump function
    public void Jump(InputAction.CallbackContext context) {
        if (context.performed && isGroundedCheck()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (context.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }


    // move function
    public void Move(InputAction.CallbackContext context) {
        horzInput = context.ReadValue<Vector2>().x;
        vertInput = context.ReadValue<Vector2>().y;
    }

    public void Dash(InputAction.CallbackContext context) {
        if (context.performed && canDash) {
            Debug.Log("dashed");
            canMove = false;
            isGrounded = false;
            isDashing = true;
            canDash = false;
            dashTrail.emitting = true;
 
            if (horzInput > 0) {
                horzInput = 1;
            } else if (horzInput < 0) {
                horzInput = -1;
            }
            if (vertInput > 0) {
                vertInput = 1;
            } else if (vertInput < 0) {
                vertInput = -1;
            }
            
            dashDirection = new Vector2(horzInput, vertInput).normalized;
            
            if(dashDirection == Vector2.zero) {
                dashDirection = transform.right;
            }

            StartCoroutine(stopDash());
        }

        
    }

    // IEnumerators

    IEnumerator gravityChange() {
        rb.gravityScale = playerGravity / 2;
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = playerGravity;
    } 

    IEnumerator stopDash() {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        canDash = false;
        canMove = true;
        isGrounded = true;
        rb.gravityScale = playerGravity;
        yield return new WaitForSeconds(0.1f);
        dashTrail.emitting = false;
    }

    IEnumerator freezeMe(float time) {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(time);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator freezeMyInputs(float time) {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
}
