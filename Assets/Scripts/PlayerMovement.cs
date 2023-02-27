using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb; 
    // public Transform groundCheck;
    // public LayerMask groundLayer;
    private TrailRenderer dashTrail;
    

    private float horzInput;
    private float vertInput;
    private bool isFacingRight = true;
    [SerializeField] private float speed; // [SerializeField lets you edit in Unity]
    
    // keys to perform actions
    //public string jumpKey;
    public string dashKey;

    // variables to control whether you can perform actions
    private bool canMove = true;
    private bool isGrounded = true;
    public bool unlockedDash = true;
    private bool canDash = false;
    private bool isDashing = false;


    // general variables
    private float playerGravity = 7.0f;

    // is grounded variables
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    // jump variables
    public float jumpForce;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    public float canJumpTime;

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

    void FixedUpdate() {
        // Vector2 for 2D movement; Input.GetAxis("Horizontal") gives -1 or 1 
        
        
    

        if (isClimbing) {
            rb.gravityScale = 0f;
            if (vertInput > 0f) {
                rb.velocity = new Vector2(rb.velocity.x, vertInput * fallSpeed);
            } else {
                rb.velocity = new Vector2(rb.velocity.x, -fallSpeed/2);
            }
            
        } else {
            rb.gravityScale = playerGravity;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       if (canMove) {
            rb.velocity = new Vector2(horzInput * speed, rb.velocity.y); 

            if (!isFacingRight && horzInput > 0f) {
                Flip();
            } else if (isFacingRight && horzInput < 0f) {
                Flip();
            }
        }

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isDashing) {
            isGrounded = false;
        }
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded) {
            canJumpTime = 0.15f;
            if (unlockedDash) {
                canDash = true;
            }
            
        } else {
            canJumpTime -= Time.deltaTime;
        }

        if (playerInput.actions["Jump"].triggered) {
            jumpBufferCounter = jumpBufferTime;
        } else {
            jumpBufferCounter -= Time.deltaTime;
        }
        

        if (jumpBufferCounter > 0f && canJumpTime > 0) {
            isJumping = true;
            jumpBufferCounter = 0f;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;        
        } 

        if (Input.GetButton("Jump") && isJumping == true) {
            rb.velocity = Vector2.up * jumpForce;
            if (jumpTimeCounter > 0f) {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;

            } else {
                isJumping = false;
            }
        }
        //playerInput.actions["Jump"].triggered

        // if player stops holding down space they aren't jumping anymore
        if (Input.GetButtonUp("Jump")) {
            isJumping = false;
            canJumpTime = 0;
            StartCoroutine(gravityChange());
        }


        // dash
        if (playerInput.actions["Dash"].triggered && canDash) {
            canMove = false;
            isGrounded = false;
            isDashing = true;
            canDash = false;
            dashTrail.emitting = true;

            horzInput = Input.GetAxis("Horizontal");
            vertInput = Input.GetAxis("Vertical");
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

        if (isDashing) {
            rb.gravityScale = 0;
            rb.velocity = dashDirection * dashVelocity;
            return;
        }
        
        // laddet

        if (onLadder && Mathf.Abs(vertInput) > 0f) {
            isClimbing = true;
        }
    }
    
    private void Flip() {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Jump(InputAction.CallbackContext context) {
        if (context.perform && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (context.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context) {
        horzInput = context.ReadValue<Vector2>().x;
    }

    // method to freeze player position 
    public void FreezePosition(float freezeTime) {
        StartCoroutine(freezeMe(freezeTime));
    }

    public void FreezeInputs(float freezeTime) {
        StartCoroutine(freezeMyInputs(freezeTime));
    }


    // ladder functions
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

