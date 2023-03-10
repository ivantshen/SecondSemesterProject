using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private SpriteRenderer sr;
    [SerializeField] private float moveSpeed = 10;
    private int currentPhase = 0;
    private bool allowMoves = true;
    public Transform player;
    private bool allowCollisionDamage = true;
    private float collisionTimer = 5f;
    [SerializeField] private float damageAmount;

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
        sr.transform.localScale = new Vector2(1.1f, 1.1f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(2.0f, 2.0f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(3.0f, 3.0f);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 15;
        currentPhase = 1;
    }

    // phase 1 moves
    IEnumerator phase1MoveChain() {
        int randomMoveNumber = Random.Range(2,2);
        if (randomMoveNumber == 1) {
            StartCoroutine(moveAround());
        } else if (randomMoveNumber == 2) {
            StartCoroutine(slam());
        } else {
            StartCoroutine(zoteSlam());
        }
        yield return new WaitForSeconds(2f);
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
        sr.color = new Color(0.5f, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = -40;
        yield return new WaitForSeconds(1f);
        rb.AddForce((new Vector2(player.position.x,0) - new Vector2(transform.position.x,0)).normalized * moveSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 1000;
        rb.velocity = Vector2.zero;
    }

    // move 3 
    IEnumerator zoteSlam() {
        Debug.Log("zoteSlam");
        sr.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = -3;
        yield return new WaitForSeconds(0.7f);
        rb.gravityScale = 0;
        rb.velocity = new Vector2(-40, 0);
        yield return new WaitForSeconds(2f);
        rb.velocity = new Vector2(40, 0);
        yield return new WaitForSeconds(2f);
        rb.gravityScale = 100;
        rb.velocity = Vector2.zero;
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
