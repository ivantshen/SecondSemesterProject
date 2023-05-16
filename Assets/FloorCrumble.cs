using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCrumble : MonoBehaviour
{
    public GameObject floor;
    [SerializeField] private AudioSource breakSoundEffect;
    private float cd;
    private SpriteRenderer sr;
    private Collider2D col;
    [SerializeField] private bool respawn = true;
    void Start(){
        col = gameObject.GetComponent<Collider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player"&&collision.gameObject.transform.position.y>transform.position.y+0.75f) {
            StartCoroutine(crumbleMyFloor());
        }
    }
    private void Update(){
        if(cd>0){
            cd-= Time.deltaTime;
        }else{
            sr.enabled = true;
            col.enabled = true;
        }
    }
    IEnumerator crumbleMyFloor() {
        yield return new WaitForSeconds(0.2f);
        breakSoundEffect.Play();
        yield return new WaitForSeconds(0.3f);
        if(respawn){
        col.enabled = false;
        sr.enabled = false;
        cd = 5;    
        }else{
        Destroy(gameObject);
        }
        
    }
    
}
