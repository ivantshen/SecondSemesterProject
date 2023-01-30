using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private Rigidbody2D rb; 
    private SpriteRenderer sr;
    private int currentPhase = 0;
    private bool allowMoves = true;
    public GameObject player;

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
        sr.transform.localScale = new Vector2(2.0f, 2.0f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(4.0f, 4.0f);
        yield return new WaitForSeconds(1f);
        sr.transform.localScale = new Vector2(6.0f, 6.0f);
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 15;
        currentPhase = 1;
    }

    // phase 1 moves
    IEnumerator phase1MoveChain() {
        int randomMoveNumber = Random.Range(1,4);
        if (randomMoveNumber == 1) {
            StartCoroutine(move1());
        } else if (randomMoveNumber == 2) {
            StartCoroutine(move2());
        } else {
            StartCoroutine(move3());
        }
        yield return new WaitForSeconds(3f);
        allowMoves = true;
    }

    // move 1
    IEnumerator move1() {
        Debug.Log("move1");
        rb.velocity = new Vector2(-30, rb.velocity.y); 
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(30, rb.velocity.y); 
        yield return new WaitForSeconds(1f);
    }

    IEnumerator move2() {
        Debug.Log("move2");
        rb.gravityScale = -3;
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 100;
    }

    IEnumerator move3() {
        Debug.Log("move3");
        rb.gravityScale = -3;
        yield return new WaitForSeconds(1f);
        rb.gravityScale = 100;
    }


}
