using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3,12,true);
        Physics2D.IgnoreLayerCollision(3,3,true);
        Physics2D.IgnoreLayerCollision(3,9,true);
        Physics2D.IgnoreLayerCollision(3,7,true);
        for(int i=0;i<13;i++){
        Physics2D.IgnoreLayerCollision(10,i,true);    
        }
        for(int i=0;i<12;i++){
        Physics2D.IgnoreLayerCollision(11,i,true);    
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Physics2D.IgnoreLayerCollision(3,12,true);
        Physics2D.IgnoreLayerCollision(3,3,true);
        Physics2D.IgnoreLayerCollision(3,9,true);
        Physics2D.IgnoreLayerCollision(3,7,true);
        for(int i=0;i<13;i++){
        Physics2D.IgnoreLayerCollision(10,i,true);    
        }
        for(int i=0;i<12;i++){
        Physics2D.IgnoreLayerCollision(11,i,true);    
        }
    }
}
