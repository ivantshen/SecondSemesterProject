using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach : MonoBehaviour
{
    public GameObject attachTo;
    public GameObject whatToAttach;
    private bool isAttached = true;
    // Update is called once per frame
    void Update()
    {
        if(isAttached){
        whatToAttach.transform.position = attachTo.transform.position;    
        }
    }
    public void toggleAttach(){
        isAttached = !isAttached;
    }
}
