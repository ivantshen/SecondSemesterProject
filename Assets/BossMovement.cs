using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private SpriteRenderer sr;
    // public Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.gravityScale = 0;
        sr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(30, rb.velocity.y); 
    }

    public void BossTransformation() {
        StartCoroutine(getHuge());

        
    }

    IEnumerator getHuge() {
        yield return new WaitForSeconds(2f);
        sr.transform.localScale = new Vector2(2.0f, 2.0f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(4.0f, 4.0f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(6.0f, 6.0f);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 15;
    }


}
