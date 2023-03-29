using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private bool unlocked;
    public static Transform lastTouched;
    private GameObject player;
    [SerializeField] bool spawnPoint;
    void Start(){
        player = GameObject.FindWithTag("Player");
        if(spawnPoint){
            lastTouched = transform;
        }
    }
    public bool getUnlockedStatus(){
        return unlocked;
    }
    public void setUnlockedStatus(bool b){
        unlocked = b;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(!unlocked){
            unlocked = true;
            lastTouched = this.gameObject.transform;
        }
    }
    public static GameObject getNearestCheckpoint(){
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        GameObject nearest = null;
        Transform player = PlayerPersistence.Instance.transform;
        float nearestDistance = float.MaxValue;
        foreach(GameObject c in checkpoints){
            if(Vector2.Distance(player.position,c.transform.position)<nearestDistance&&c.GetComponent<Checkpoint>().getUnlockedStatus()){
                nearestDistance = Vector2.Distance(player.position,c.transform.position);
                nearest = c;
            }
        }
        return nearest;
    }
}
