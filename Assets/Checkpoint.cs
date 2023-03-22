using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    private GameObject player;
    void Start(){
        player = GameObject.FindWithTag("Player");
    }
    public bool getUnlockedStatus(){
        return unlocked;
    }
    public void setUnlockedStatus(bool b){
        unlocked = b;
    }
    void Update(){
        if(!unlocked&&Vector2.Distance(player.transform.position,transform.position)<10){
            unlocked = true;
        }
    }
}
