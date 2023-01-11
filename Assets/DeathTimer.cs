using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float timeTillDeath = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if(timeTillDeath>0){
            timeTillDeath-= Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
    }
}
