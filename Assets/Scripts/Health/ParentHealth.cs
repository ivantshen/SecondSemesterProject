using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ParentHealth : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth {get; private set;}
    public int monAmt;
    private int sceneIndex;
    [SerializeField] private bool boss = false;
    [SerializeField] private int scoreBoard;
    private FireBaseLeaderboard lb;

    private void Awake(){
        currentHealth = startingHealth;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        lb = FireBaseLeaderboard.Instance;

    }

    public void TakeDamage(float _damage){
        
        if (currentHealth > 0){
            currentHealth = Mathf.Clamp(currentHealth - _damage,0 , startingHealth);
            if(currentHealth <=0){
                if(gameObject.tag=="Nonpermanent"){
                ScenePersist.scenes[sceneIndex].setPickedUp(gameObject);    
                }
                
                
                CurrencyManager.keyy.AddMon(monAmt);
                //if(this.layer == 6){
                    lb.changeScore(scoreBoard);
                //}
                if(boss){
                    SceneManager.LoadScene(9);
                }else{
                    Destroy(gameObject);
                }
                //XPManager.instance.AddXP(monAmt);
            }
        }
        else{
            
        }
    }

    public float getHealth(){
        return currentHealth;
    }
    public float getStart(){
        return startingHealth;
    }
    
}
