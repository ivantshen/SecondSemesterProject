using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool knockbackeable = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void knockback(float force,Vector2 direction){
        if(knockbackeable){
            rb.AddForce(force*direction,ForceMode2D.Impulse);
        }
    }
}
