using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXFollowsPlayer : MonoBehaviour
{
    public VisualEffect vfx;
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        if(player){
        vfx.SetVector3("colliderPos",player.position);    
        }
    }
}
