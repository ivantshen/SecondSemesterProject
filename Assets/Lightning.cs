using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float targetRadius;
    [SerializeField] private int damage;
    [SerializeField] int targetLayer = 6;
    [SerializeField] GameObject lightning;
    private Transform player;
    private ComboManager cm;
    private Collider2D thisCollider;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        targetLayer = LayerMask.GetMask("Enemies");
        cm = player.gameObject.GetComponent<ComboManager>();
        thisCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer==6){
            float angle = Mathf.Atan2(transform.position.y-other.transform.position.y,transform.position.x-other.transform.position.x);
            GameObject l = Instantiate(lightning,transform.position,Quaternion.Euler(0,0,angle));
            Destroy(l,0.3f);
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            cm.increaseHitcount(1);
        }
    }
}
