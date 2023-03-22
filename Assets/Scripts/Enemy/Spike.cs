using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(other.gameObject){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            other.gameObject.GetComponent<HealthP1>().resetHealth();
            teleportNearestCheckpoint(other.gameObject.transform);
            }
        }
     }

    private void teleportNearestCheckpoint(Transform player){
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        GameObject nearest = null;
        float nearestDistance = float.MaxValue;
        foreach(GameObject c in checkpoints){
            if(Vector2.Distance(player.position,c.transform.position)<nearestDistance&&c.GetComponent<Checkpoint>().getUnlockedStatus()){
                nearestDistance = Vector2.Distance(player.position,c.transform.position);
                nearest = c;
            }
        }
        player.position = nearest.transform.position;
    }
     
}
