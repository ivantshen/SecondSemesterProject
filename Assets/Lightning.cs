using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float targetRadius;
    [SerializeField] private int damage;
    [SerializeField] private GameObject lightning;
    [SerializeField] private float knockdownForce;
    private Transform player;
    private ComboManager cm;
    private Collider2D thisCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        cm = player.gameObject.GetComponent<ComboManager>();
        thisCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==6){
            float angle = Mathf.Atan2(other.transform.position.y-transform.position.y,other.transform.position.x-transform.position.x)*Mathf.Rad2Deg;
            GameObject l = Instantiate(lightning,transform.position,Quaternion.Euler(0,0,angle));
            cm.increaseHitcount(1);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-1)*knockdownForce,ForceMode2D.Impulse);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(l,0.3f);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.layer==6){
            float angle = Mathf.Atan2(other.transform.position.y-transform.position.y,other.transform.position.x-transform.position.x)*Mathf.Rad2Deg;
            GameObject l = Instantiate(lightning,transform.position,Quaternion.Euler(0,0,angle));
            cm.increaseHitcount(1);
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-1)*knockdownForce,ForceMode2D.Impulse);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(l,0.3f);
        }
    }
}
