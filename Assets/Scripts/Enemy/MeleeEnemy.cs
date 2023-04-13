using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    public Transform firePoint;
    public GameObject slash;
    private bool canAttack = true;
    private bool canRun = true;
    private float cooldownTimer = Mathf.Infinity;
    private GameObject player;
    private float distance;
    [SerializeField] private SpriteRenderer sr;
    private Rigidbody2D rb;


    [SerializeField] GameObject enemy;
    [SerializeField] float leftEdge;
    [SerializeField] float rightEdge;
    [SerializeField] private float speed;
    private bool movingLeft;

    private void Start(){
        player = PlayerPersistence.Instance;
        rb = GetComponent<Rigidbody2D>();
        //sr = GetComponent<SpriteRenderer>();
    }

    private void Update(){
        distance = Vector2.Distance(transform.position, player.transform.position);
        //Vector2 direction = player.transform.position - transform.position;
        //direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        cooldownTimer += Time.deltaTime;
        //if player is too far so it goes patrolling
        if(distance > 10){
            sr.color = new Color(1f, 1f, 1f, 1);
            if(movingLeft){
                if(transform.position.x > leftEdge){
                    transform.position = new Vector2(enemy.transform.position.x - Time.deltaTime * speed,enemy.transform.position.y);
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    
                }
                else{
                    movingLeft = !movingLeft;

                }
            }
            else{
                if( transform.position.x < rightEdge){
                    transform.position = new Vector2(enemy.transform.position.x + Time.deltaTime * speed,enemy.transform.position.y);
                    transform.eulerAngles = new Vector3(0, 180, 0);

                }
                else{
                    movingLeft = !movingLeft;
                }
            }
        }
        //if player is in range
        else{
            
            if(PlayerInSight()){
            if(cooldownTimer >= attackCooldown){
            
                if(canAttack){
                cooldownTimer = 0;
                StartCoroutine(Slash());
                }
            
            
            }
            }
            else{
                sr.color = new Color(0.5f, 0.5f, 0.5f, 1);
                //if(canRun){
             //StartCoroutine(runToPlayer());
             //}
             if(player.transform.position.x > transform.position.x){
            transform.position = new Vector2(enemy.transform.position.x + Time.deltaTime * speed * 2f,enemy.transform.position.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else {
            transform.position = new Vector2(enemy.transform.position.x - Time.deltaTime * speed * 2f,enemy.transform.position.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
            }
            }
        }
        
            
        
    }

    private bool PlayerInSight(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
         new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
          0, Vector2.left, 0, playerLayer );

        return hit.collider != null;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right *range* transform.localScale.x * colliderDistance,
         new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private IEnumerator Slash(){
        canAttack = false;
        //make slash
        if(player.transform.position.x > enemy.transform.position.x){
            
            sr.color = new Color(255f, 0f, 0f, 1);
            yield return new WaitForSeconds(0.4f);
            rb.velocity = new Vector2(15f,0);
            yield return new WaitForSeconds(0.1f);
            GameObject newSlash = Instantiate(slash,firePoint.position,Quaternion.Euler(new Vector3(0,0,0)),null);
            damagePlayer();
            
            
            
            yield return new WaitForSeconds(0.15f);
            
            GameObject newSlash2 = Instantiate(slash,firePoint.position,Quaternion.Euler(new Vector3(0,0,0)),null);
            damagePlayer();
            Destroy(newSlash,0.25f);
            Destroy(newSlash2,0.25f);
            rb.velocity = new Vector2(0,0);
        }
        else if(player.transform.position.x < enemy.transform.position.x){
            
            sr.color = new Color(255f, 0f, 0f, 1);
            yield return new WaitForSeconds(0.4f);
            rb.velocity = new Vector2(-15f,0);
            yield return new WaitForSeconds(0.1f);
            GameObject newSlash = Instantiate(slash,firePoint.position,Quaternion.Euler(new Vector3(0,180,0)),null);
            damagePlayer();
            yield return new WaitForSeconds(0.15f);
            
            GameObject newSlash2 = Instantiate(slash,firePoint.position,Quaternion.Euler(new Vector3(0,180,0)),null);
            damagePlayer();
            Destroy(newSlash,0.25f);
            Destroy(newSlash2,0.25f);
            
            rb.velocity = new Vector2(0,0);
        }
        


        //transform.rotation = Quaternion.Euler(new Vector3(0,180,0));
        

        
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

    private void damagePlayer(){
        Collider2D[] playerToDmg = Physics2D.OverlapCircleAll(firePoint.position,2,playerLayer);
        foreach (Collider2D Player in playerToDmg){
        if(Player){
            Debug.Log("works");
            Player.gameObject.GetComponent<HealthP1>().TakeDamage(10);
                //Player.GetComponent<EnemyHealth>().TakeDamage(10);
        }
        }
    }


    //not used rn
    private IEnumerator runToPlayer(){
        canRun = false;
        //Debug.Log("running");
        if(player.transform.position.x > transform.position.x){
            transform.position = new Vector2(enemy.transform.position.x + Time.deltaTime * speed * 2f,enemy.transform.position.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else {
            transform.position = new Vector2(enemy.transform.position.x - Time.deltaTime * speed * 2f,enemy.transform.position.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        yield return new WaitForSeconds(0.01f);
        canRun = true;
    }
}
