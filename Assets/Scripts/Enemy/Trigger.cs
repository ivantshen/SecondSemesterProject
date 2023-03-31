using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    public int airForce;
    [SerializeField] private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.tag == "Player"){
        
        other.GetComponent<Rigidbody2D>().AddForce(direction * airForce * Time.deltaTime);
        }
    }
}
