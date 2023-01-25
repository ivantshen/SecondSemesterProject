using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private TrailRenderer dashTrail;
    
    [SerializeField] private float speed; // [SerializeField lets you edit in Unity]
    
    // variables to control whether you can perform actions
    private bool canMove = true;
    private bool isGrounded = true;
    private bool canDash = true;
    private bool isDashing = false;

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
    private float horzInput;
    private float vertInput;
    
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // GetComponent looks for the component in the inspector
        dashTrail = GetComponent<TrailRenderer>();
        dashTrail.emitting = false;
        
    }

    void FixedUpdate() {
        // Vector2 for 2D movement; Input.GetAxis("Horizontal") gives -1 or 1 
        if (canMove) {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y); 
        }
        
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
        if (isDashing) {
            isGrounded = false;
        }
        //isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded) {
            canJumpTime = 0.15f;
            canDash = true;
        } else {
            canJumpTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
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

        if (Input.GetKey(KeyCode.Space) && isJumping == true) {
            rb.velocity = Vector2.up * jumpForce;
            if (jumpTimeCounter > 0f) {
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


        // dash
        if (Input.GetKeyDown(KeyCode.X) && canDash) {
            canMove = false;
            isGrounded = false;
            isDashing = true;
            canDash = false;
            dashTrail.emitting = true;

            // if (Input.GetKey(KeyCode.LeftArrow)) {
            //     horzInput = -1;
            // } else if (Input.GetKey(KeyCode.RightArrow)) {
            //     horzInput = 1;
            // } else {
            //     horzInput = 0;
            // }

            // if (Input.GetKey(KeyCode.UpArrow)) {
            //     vertInput = 0.9f;
            // } else if (Input.GetKey(KeyCode.DownArrow)) {
            //     vertInput = -1;
            // } else {
            //     vertInput = 0;
            // }
            
            // dashDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
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
        
        
    }

    // method to freeze player position 
    public void FreezePosition() {
        StartCoroutine(freezeMe());
    }


    // IEnumerators

    IEnumerator gravityChange() {
        rb.gravityScale = 3.5f;
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = 7;
    } 

    IEnumerator stopDash() {
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        canDash = false;
        canMove = true;
        isGrounded = true;
        rb.gravityScale = 7;
        yield return new WaitForSeconds(0.1f);
        dashTrail.emitting = false;
    }

    IEnumerator freezeMe() {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}

