using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private FireBaseLeaderboard fblb;
    void Awake(){
        fblb = FireBaseLeaderboard.Instance;
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(other.gameObject){
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            other.gameObject.transform.position = Checkpoint.lastTouched[SceneManager.GetActiveScene().buildIndex].position;
            }
        }
    }
}
