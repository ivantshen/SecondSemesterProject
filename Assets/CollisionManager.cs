using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3,3,true);
        Physics2D.IgnoreLayerCollision(3,9,true);
        Physics2D.IgnoreLayerCollision(3,7,true);
        for(int i=0;i<11;i++){
        Physics2D.IgnoreLayerCollision(10,i,true);    
        }
        
    }
}
