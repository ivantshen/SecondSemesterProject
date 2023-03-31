using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkpoint : MonoBehaviour
{
    private int sceneIndex;
    [SerializeField] private bool unlocked;
    public static Transform[] lastTouched;
    private GameObject player;
    [SerializeField] bool spawnPoint;
    void Start(){
        if(lastTouched==null||lastTouched.Length==0){
            lastTouched = new Transform[SceneManager.sceneCountInBuildSettings];
        }
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        player = GameObject.FindWithTag("Player");
        if(spawnPoint&&!lastTouched[sceneIndex]){
            lastTouched[sceneIndex] = transform;
        }
    }
    public bool getUnlockedStatus(){
        return unlocked;
    }
    public void setUnlockedStatus(bool b){
        unlocked = b;
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
        if(!unlocked){
            unlocked = true;
        }
        lastTouched[sceneIndex] = this.gameObject.transform;
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
