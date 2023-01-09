using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right*bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="Player"){
            other.gameObject.GetComponent<HealthP1>().TakeDamage(10);
        }
        Destroy(gameObject);
    }
}
