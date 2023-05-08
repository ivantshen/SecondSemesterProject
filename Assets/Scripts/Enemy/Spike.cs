using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spike : MonoBehaviour
{
    private FireBaseLeaderboard fblb;
    void Start(){
        fblb = FireBaseLeaderboard.Instance;
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            if(other.gameObject){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            fblb.addDeaths(1);
            other.gameObject.GetComponent<HealthP1>().resetHealth();
            other.gameObject.transform.position = Checkpoint.lastTouched[SceneManager.GetActiveScene().buildIndex].position;
            }
        }
     }
     
}
