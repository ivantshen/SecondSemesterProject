using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoneyOnTouch : MonoBehaviour
{
    private CurrencyManager crm;
    [SerializeField] private int amt;
    private int sceneIndex;
    private FireBaseLeaderboard fblb;
    void Start(){
        fblb = FireBaseLeaderboard.Instance;
        crm = CurrencyManager.keyy;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="Player"){
            crm.AddMon(amt);
            fblb.changeScore(amt/5);
            ScenePersist.scenes[sceneIndex].setPickedUp(gameObject);
            Destroy(gameObject);
        }
    }
}
