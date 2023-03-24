using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterBoss : MonoBehaviour
{
    private Rigidbody2D rb; 
    private SpriteRenderer sr;
    
    private int currentPhase = 0;
    private bool allowMoves = true;
    public Transform player;
    public Transform center;
    private bool allowCollisionDamage = true;
    private float collisionTimer = 5f;
    private bool canAttack = true;

    // boss variables
    [SerializeField] private float damageAmount;
    [SerializeField] private float moveSpeed = 1000f;
    private float originalGravity = 100f;

    // gravity
    [SerializeField] private float floatGravity = -40f;
    [SerializeField] private float slamGravity = 1000f;

    private float distance;
    private float xPos;
    private float yPos;
    private bool canDash = false;
    private bool canFollow = false;
    private bool untouched;

    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject leftAttack;
    [SerializeField] private GameObject rightAttack;
    [SerializeField] private Transform firePoint;
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


        if(canDash){
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(xPos,yPos), moveSpeed/20 * Time.deltaTime);
        }


        if(canFollow){
        //while(untouched){
            distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed/500 * Time.deltaTime);

        
        //}
        }
        
    }

    public void BossTransformation() {
        StartCoroutine(getHuge());
        
        
    }

    // makes him larger at the start
    IEnumerator getHuge() {
        yield return new WaitForSeconds(2f);
        sr.transform.localScale = new Vector2(1.5f, 1.5f);
        //yield return new WaitForSeconds(0.5f);
        //sr.transform.localScale = new Vector2(2.25f, 2.25f);
        //yield return new WaitForSeconds(0.5f);
        sr.transform.localScale = new Vector2(3.0f, 3.0f);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 15;
        currentPhase = 1;
        yield return new WaitForSeconds(0.8f);
    }

    // phase 1 moves
    IEnumerator phase1MoveChain() {
        int randomMoveNumber = Random.Range(1,9);
        if(canAttack){
        if (randomMoveNumber < 2) {
            StartCoroutine(diagonalSlam());
            
        } else if (randomMoveNumber < 6) {
            StartCoroutine(fire());
            
        } else {
            StartCoroutine(slam());
            
        }
        }
        yield return new WaitForSeconds(3f);
        allowMoves = true;
    }

    // move 1
    /* IEnumerator moveAround() {
        Debug.Log("moveAround");
        rb.gravityScale = originalGravity;
        sr.color = new Color(0,0,0,1);
        //rb.gravityScale = 100;
        yield return new WaitForSeconds(0.5f);

        //rb.velocity = new Vector2();
        rb.AddForce((new Vector2(player.position.x,0) - new Vector2(transform.position.x,0)).normalized * moveSpeed, ForceMode2D.Impulse);
        //rb.gravityScale = originalGravity;
        yield return new WaitForSeconds(0.5f);
    } */
    IEnumerator diagonalSlam() {
        Debug.Log("moveAround");
        canAttack = false;
        rb.gravityScale = originalGravity;
        sr.color = new Color(0,0,0,1);
        rb.gravityScale = floatGravity;
        yield return new WaitForSeconds(0.7f);
        rb.gravityScale = 0;
        //transform.position = new Vector2(-13.6f, 13f);
        
            yield return new WaitForSeconds(1f);

        canDash = true;
        xPos = player.position.x;
        yPos = player.position.y;

       
        yield return new WaitForSeconds(0.2f);
        canDash = false;
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = originalGravity;
        canAttack = true;
    } 
    // 20 2000 -40 1000


    // move 2
     IEnumerator slam() {
        Debug.Log("slam");
        rb.gravityScale = originalGravity;
        
        canAttack = false;
        sr.color = new Color(0.5f, 0.5f, 0.5f, 1);
        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;
        rb.gravityScale = floatGravity;
        yield return new WaitForSeconds(0.8f);
        
        rb.gravityScale = 0f;
        transform.position = new Vector2(player.position.x, 13f);
        yield return new WaitForSeconds(0.3f);

        rb.gravityScale = slamGravity;
        yield return new WaitForSeconds(0.2f);
        Instantiate(leftAttack,firePoint.position,firePoint.rotation);
        Instantiate(rightAttack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(0.3f);
        
        canAttack = true;

        //rb.velocity = Vector2.zero;
    }

    IEnumerator fire() {
        Debug.Log("fire");
        rb.gravityScale = originalGravity;
        //untouched = true;
        canAttack = false;
        sr.color = new Color(1f, 1f, 1f, 1);
        rb.gravityScale = floatGravity;
        yield return new WaitForSeconds(0.7f);
        rb.gravityScale = 0;
        //transform.position = new Vector2(-13.6f, 13f);
        canFollow = true;
        yield return new WaitForSeconds(1f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(0.5f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(5f);
        canAttack = true;
        canFollow = false;
        //untouched = true;

        
    }
 
    // move 3 
     IEnumerator zoteSlam() {
        Debug.Log("zoteSlam");
        rb.gravityScale = originalGravity;
        canAttack = false;
        sr.color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(0.5f);

        transform.position = new Vector2(-13.6f, 13f);    
        rb.gravityScale = 0;

        yield return new WaitForSeconds(0.3f);
        sr.transform.localScale = new Vector2(20f, 20f);
        yield return new WaitForSeconds(1.5f);
        sr.transform.localScale = new Vector2(3f, 3f);

        yield return new WaitForSeconds(0.5f);
        canAttack = true;
    } 

    private void OnCollisionStay2D(Collision2D other){
        if (other.gameObject.tag == "Player" && allowCollisionDamage) {
            if (other.gameObject) {
                allowCollisionDamage = false;
                collisionTimer = 5f;
                other.gameObject.GetComponent<HealthP1>().TakeDamage(damageAmount);
                    
            }
            if(other.gameObject.layer == 8){
                canDash = false;
                canFollow = false;
            }
             
        }
     }
}
