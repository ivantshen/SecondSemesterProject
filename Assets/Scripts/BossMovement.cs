using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private SpriteRenderer sr;
    
    private int currentPhase = 0;
    private bool allowMoves = true;
    public Transform player;
    public Transform center;
    private bool allowCollisionDamage = true;
    private float collisionTimer = 5f;

    // boss variables
    [SerializeField] private float damageAmount;
    [SerializeField] private float moveSpeed = 1000f;

    // gravity
    [SerializeField] private float floatGravity = -40f;
    [SerializeField] private float slamGravity = 1000f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.gravityScale = 0;
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionTimer > 0) {
            collisionTimer -= Time.deltaTime;
        } else {
            allowCollisionDamage = true;
        }

        // phase 1
        if (allowMoves && currentPhase == 1) {
            allowMoves = false;
            StartCoroutine(phase1MoveChain());
        }
    }

    public void BossTransformation() {
        StartCoroutine(getHuge());
        
        
    }

    // makes him larger at the start
    IEnumerator getHuge() {
        yield return new WaitForSeconds(2f);
        sr.transform.localScale = new Vector2(1.5f, 1.5f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(2.25f, 2.25f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(3.0f, 3.0f);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 15;
        currentPhase = 1;
        yield return new WaitForSeconds(0.8f);
    }

    // phase 1 moves
    IEnumerator phase1MoveChain() {
        yield return new WaitForSeconds(1.5f);
        int randomMoveNumber = Random.Range(1,4);
        if (randomMoveNumber == 1) {
            StartCoroutine(moveAround());
        } else if (randomMoveNumber == 2) {
            StartCoroutine(slam());
        } else {
            StartCoroutine(zoteSlam());
        }
        yield return new WaitForSeconds(0.5f);
        allowMoves = true;
    }

    // move 1
    IEnumerator moveAround() {
        Debug.Log("moveAround");
        sr.color = new Color(0,0,0,1);
        rb.gravityScale = 100;
        yield return new WaitForSeconds(0.5f);

        //rb.velocity = new Vector2();
        rb.AddForce((new Vector2(player.position.x,0) - new Vector2(transform.position.x,0)).normalized * moveSpeed, ForceMode2D.Impulse);
    }


    // move 2
    IEnumerator slam() {
        Debug.Log("slam");
        rb.gravityScale = 100f;
        sr.color = new Color(0.5f, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;
        rb.gravityScale = floatGravity;
        yield return new WaitForSeconds(0.8f);
        
        rb.gravityScale = 0f;
        transform.position = new Vector2(player.position.x, transform.position.y);
        yield return new WaitForSeconds(0.3f);

        rb.gravityScale = slamGravity;
        rb.velocity = Vector2.zero;
    }

    // move 3 
    IEnumerator zoteSlam() {
        Debug.Log("zoteSlam");
        sr.color = new Color(255, 255, 255, 1);
        rb.gravityScale = 0;
        transform.position = new Vector2(-13.6f, 13f);
        yield return new WaitForSeconds(0.3f);
        sr.transform.localScale = new Vector2(20f, 20f);
        yield return new WaitForSeconds(1.5f);
        sr.transform.localScale = new Vector2(3f, 3f);

    }

    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.tag == "Player" && allowCollisionDamage) {
            if (other.gameObject) {
                allowCollisionDamage = false;
                collisionTimer = 5f;
                other.gameObject.GetComponent<HealthP1>().TakeDamage(damageAmount);    
            }
             
        }
     }

}
