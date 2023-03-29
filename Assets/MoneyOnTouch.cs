using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoneyOnTouch : MonoBehaviour
{
    private CurrencyManager crm;
    [SerializeField] private int amt;
    private int sceneIndex;
    void Start(){
        crm = CurrencyManager.keyy;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag=="Player"){
            crm.AddMon(amt);
            ScenePersist.scenes[sceneIndex].setPickedUp(gameObject);
            Destroy(gameObject);
        }
    }
}
