using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ar : MonoBehaviour
{
    [SerializeField] private int minimumDamage;
    [SerializeField] private int maximumDamage;
    [SerializeField] private float arrowMinimumVelocity;
    [SerializeField] private float arrowMaximumVelocity;
    [SerializeField] private bool piercing;
    [SerializeField] private float percentDamageLossPerTargetHit;
    [SerializeField] private float percentVelocityLossPerTargetHit;
    private GameObject attachedTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
