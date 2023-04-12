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
    private float cooldownTimer = Mathf.Infinity;
    private GameObject player;

    private void Start(){
        player = PlayerPersistence.Instance;
    }

    private void Update(){
        cooldownTimer += Time.deltaTime;
        if(PlayerInSight()){
        if(cooldownTimer >= attackCooldown){
            if(canAttack){
            cooldownTimer = 0;
            StartCoroutine(Slash());
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
        GameObject newSlash = Instantiate(slash,firePoint.position,firePoint.rotation,null);
        Collider2D[] playerIn = Physics2D.OverlapCircleAll(firePoint.position,3,playerLayer);
        if(playerIn[0] == player){
            Debug.Log("works");
        }
        

        Destroy(newSlash,0.25f);
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
}
