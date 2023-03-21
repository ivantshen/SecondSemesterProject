using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPersistence : MonoBehaviour
{
    public static GameObject Instance;
    // Start is called before the first frame update
    void Awake(){
        if(Instance){
            Destroy(gameObject);
            return;
        }
        Instance = this.gameObject;
        DontDestroyOnLoad(gameObject);
    }
}
