using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterBoss : MonoBehaviour
{
    private Rigidbody2D rb; 
    private SpriteRenderer sr;
    private EnemyHealth eH;
    
    private int currentPhase = 0;
    private bool allowMoves = true;
    public Transform player;
    public Transform center;
    private bool allowCollisionDamage = true;
    private float collisionTimer = 5f;
    private bool canAttack = true;
    public float knockbackForce;

    // boss variables
    [SerializeField] private float damageAmount;
    [SerializeField] private float moveSpeed = 1000f;
    private float originalGravity = 100f;

    // gravity
    [SerializeField] private float floatGravity = -40f;
    [SerializeField] private float slamGravity = 1000f;

    private float distance;
    private float distancePlayer;
    private float distanceBoss;
    private float xPos;
    private float yPos;
    private bool canDash = false;
    private bool canFollow = false;
    private bool canSpawn = true;
    private bool untouched;
    //private float current;
    //private float starting;

    [SerializeField] private GameObject attack;
    [SerializeField] private GameObject leftAttack;
    [SerializeField] private GameObject rightAttack;
    [SerializeField] private Transform firePoint;

    [SerializeField] private GameObject enemy1;
    public GameObject check;
    private float randomXNumber;
    private bool phaseChange = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.gravityScale = 0;
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        eH = GetComponent<EnemyHealth>();
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
        else if(currentPhase == 2){
            allowMoves = false;
            StartCoroutine(phase2MoveChain());
            
        }

        if(currentPhase == 2 && canSpawn){
            canSpawn = false;
            StartCoroutine(spawnEnemy());
        }

        if(eH.getHealth() <= eH.getStart()/2){
            //Debug.Log("half health");
            currentPhase = 2;
        }


        if(canDash){
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(xPos,yPos), moveSpeed/20 * Time.deltaTime);
        }


        if(canFollow){
        
            distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed/500 * Time.deltaTime);

        
        
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
        
        int randomMoveNumber = Random.Range(1,11);
        if(canAttack){
        if (randomMoveNumber <5) {
            StartCoroutine(diagonalSlam());
            
        } else if (randomMoveNumber < 7) {
            StartCoroutine(fire());
            
        } else {
            StartCoroutine(slam());
            
        }
        //}
        yield return new WaitForSeconds(3f);
        
        }
        allowMoves = true;
        
    }

    IEnumerator phase2MoveChain() {
        if(phaseChange){
            //phaseChange = false;
            //canAttack = false;
             transform.position = new Vector2(-13.6f, 13f);
        rb.gravityScale = 0;
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(3.5f, 3.5f);
        sr.color = new Color(0,0,0,1);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(0.1f,0.1f,0.1f,1);
        yield return new WaitForSeconds(0.1f);
        sr.color = new Color(0.3f,0.3f,0.3f,1);
        yield return new WaitForSeconds(3f);
        rb.gravityScale = originalGravity;
        //canAttack = true;
        //yield return new WaitForSeconds(3f);
        phaseChange = false;
        }
       
        int randomMoveNumber = Random.Range(1,11);
        if(canAttack){
        if (randomMoveNumber <5) {
            StartCoroutine(diagonalSlam2());
            
        } else if (randomMoveNumber < 7) {
            StartCoroutine(fire());
            
        } else {
            StartCoroutine(slam2());
            
        }
        //}
        yield return new WaitForSeconds(3f);
        
        }
        allowMoves = true;
        
    }

    IEnumerator spawnEnemy(){
        
        Check();
        yield return new WaitForSeconds(1f);
        Instantiate(enemy1,new Vector2(randomXNumber,3f),firePoint.rotation);
        yield return new WaitForSeconds(15f);
        canSpawn = true;
        
    }

    void Check(){
        randomXNumber = Random.Range(-36f,9f);
        //check.transform.position = new Vector2(randomXNumber,3f);
        distancePlayer = Vector2.Distance(new Vector2(randomXNumber,3f), player.transform.position);
        distanceBoss = Vector2.Distance(new Vector2(randomXNumber,3f), transform.position);
        if(distancePlayer < 3 || distanceBoss < 3){
            Check();
        }
    }

    
    IEnumerator diagonalSlam() {
        Debug.Log("moveAround");
        canAttack = false;
        rb.gravityScale = originalGravity;
        sr.color = new Color(0,0,0,1);
        rb.gravityScale = floatGravity;
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 0;
        //transform.position = new Vector2(-13.6f, 13f);
        
        yield return new WaitForSeconds(1f);

        canDash = true;
        xPos = player.position.x;
        yPos = player.position.y;

       
        yield return new WaitForSeconds(0.3f);
        canDash = false;
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = originalGravity;
        canAttack = true;
    } 

    IEnumerator diagonalSlam2() {
        Debug.Log("moveAround");
        canAttack = false;
        rb.gravityScale = originalGravity;
        sr.color = new Color(0,0,0,1);
        rb.gravityScale = floatGravity;
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = 0;
        //transform.position = new Vector2(-13.6f, 13f);
        yield return new WaitForSeconds(1f);
        canDash = true;
        xPos = player.position.x;
        yPos = player.position.y;
        yield return new WaitForSeconds(0.3f);
        canDash = false;
        yield return new WaitForSeconds(0.3f);

        canDash = true;
        xPos = player.position.x;
        yPos = player.position.y;
        yield return new WaitForSeconds(0.3f);
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
        yield return new WaitForSeconds(0.18f);
        Instantiate(leftAttack,firePoint.position,firePoint.rotation);
        Instantiate(rightAttack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(0.3f);
        
        canAttack = true;

        //rb.velocity = Vector2.zero;
    }

     IEnumerator slam2() {
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
        rb.gravityScale = slamGravity * 1.3f;
        yield return new WaitForSeconds(0.18f);
        Instantiate(leftAttack,firePoint.position,firePoint.rotation);
        Instantiate(rightAttack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(0.3f);

        rb.velocity = Vector2.zero;
        rb.gravityScale = floatGravity;
        yield return new WaitForSeconds(0.8f);
        rb.gravityScale = 0f;
        transform.position = new Vector2(player.position.x, 13f);
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = slamGravity * 1.3f;
        yield return new WaitForSeconds(0.18f);
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
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = 0;
        //transform.position = new Vector2(-13.6f, 13f);
        canFollow = true;
        yield return new WaitForSeconds(1f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(attack,firePoint.position,firePoint.rotation);
        yield return new WaitForSeconds(1f);
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
                //player.GetComponent<KnockbackManager>().knockback(knockbackForce,(player.transform.position-transform.position).normalized);
            }
            if(other.gameObject.layer == 8){
                canDash = false;
                canFollow = false;
            }
             
        }
     }
}
