using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float targetRadius;
    private bool canAttack = true;
    private LayerMask targetLayer;
    private Transform player;
    private ComboManager cm;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        targetLayer = LayerMask.GetMask("Enemies");
        cm = player.gameObject.GetComponent<ComboManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canAttack){

        }
    }
}
