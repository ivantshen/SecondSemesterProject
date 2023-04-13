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

    // audios wow
    [SerializeField] private AudioSource dashSoundEffect;


    // i love input system
    [SerializeField] private PlayerInput playerInput = null;
    [SerializeField] private CharacterController controller = null;
    private Vector3 inputMovement;
    public PlayerInput PlayerInput => playerInput;


    private float horzInput;
    private float vertInput;
    private float speed = 10f;
    public bool isFacingRight = true;

    // variables to control whether you can perform actions
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] public bool unlockedDash = true;
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool isDashing = false;
    [SerializeField] private bool inAir = false;

    // general variables
    private float playerGravity = 7.0f;

    // jump variables
    [SerializeField] private float jumpForce = 20f;
    public bool jumpPressed;
    private float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 1f;
    private float jumpBufferCounter;

    // dash variables
    [SerializeField] private float dashVelocity = 20f;
    [SerializeField] private float dashTime = 0.25f;
    private Vector2 dashDirection;

    // ladder climb variables
    private float fallSpeed = 8f;
    private bool onLadder;
    private bool isClimbing;
    private bool canKnock = true;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // GetComponent looks for the component in the inspector
        dashTrail = GetComponent<TrailRenderer>();
        dashTrail.emitting = false;

        // playerInput = GetComponent<PlayerInput>();
    }

    // fixed update
    void FixedUpdate() {
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
        
        // flipppp
        if (horzInput > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = true;
        } else if (horzInput < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = false;
        }

        // halve gravity but it works for like 1 frame for some reason lol 
        if (inAir) {
            if (rb.velocity.y <= 0f) {
                StartCoroutine(gravityChange());
                inAir = false;
            }
            
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        // var finalMovement = inputMovement;
        // finalMovement *= speed * Time.deltaTime;
        // controller.Move(finalMovement);

        if(canKnock){

        if (canMove) {
            rb.velocity = new Vector2(horzInput * speed, rb.velocity.y);
        }
        
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);


        // is grounded checks
        if (isGrounded) {
            coyoteTimeCounter = coyoteTime;
            if (unlockedDash) {
                canDash = true;
            }
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }



        // dash
        if (isDashing) {
            canDash = false;
            isGrounded = false;
            rb.gravityScale = 0;
            rb.velocity = dashDirection * dashVelocity;
            return;
        }

        

        // ladder
        if (onLadder && Mathf.Abs(vertInput) > 0f) {
            isClimbing = true;
        }

        }

    }

    // jump function
    public void Jump(InputAction.CallbackContext context) {
        // if (context.performed) {
        //     jumpBufferCounter = jumpBufferTime;
        // } else {
        //     jumpBufferCounter -= Time.deltaTime;
        // }

        if (context.performed && coyoteTimeCounter > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
            inAir = true;
        }

        if (context.canceled && rb.velocity.y > 0f) {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.8f);
            coyoteTimeCounter = 0f;
        }
        
        
    }


    // move function
    public void Move(InputAction.CallbackContext context) {
        horzInput = context.ReadValue<Vector2>().x;
        vertInput = context.ReadValue<Vector2>().y;
        inputMovement = new Vector2(horzInput, 0);
    }

    public void Dash(InputAction.CallbackContext context) {
        if (context.performed && canDash) {
            dashSoundEffect.Play();
            rb.gravityScale = 0;
            canMove = false;
            canDash = false;
            isGrounded = false;
            isDashing = true;
            
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

    public void FreezeInputs(float freezeTime) {
        StartCoroutine(freezeMe(freezeTime));
    }

    public void DashRefill() {
        if (canDash == false) {
            canDash = true;
        }
    }

    public void NotOnDash() {
        // if (canDash == false) {
            // lol this is useless GG
        // }
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
        rb.gravityScale = playerGravity / 2f;
        yield return new WaitForSeconds(20f);
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

    public IEnumerator freezeMe(float time) {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(time);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator freezeMyInputs(float time) {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    IEnumerator Knocked(){
        canKnock = false;
        canMove = false;
        rb.velocity = new Vector2(0,0);
        rb.AddForce(50 * ((transform.position-transform.position).normalized),ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        canMove = true;
        canKnock = true;
    }

    //knockback function
    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.layer == 6){
            StartCoroutine(Knocked());
            rb.AddForce(5 * ((transform.position-other.transform.position).normalized),ForceMode2D.Impulse);
            //Debug.Log("ugh");
        }
    }


}
