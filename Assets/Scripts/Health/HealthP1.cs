using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthP1 : MonoBehaviour
{
   
     [SerializeField] private float iFrameTime;
     private bool allowDmg;
    private FireBaseLeaderboard fblb;
    private ComboManager cm;
    public float startingHealth;
    public float currentHealth {get; private set;}

    private void Awake(){
        allowDmg = true;
        currentHealth = startingHealth;
        Physics2D.IgnoreLayerCollision(0,6,false);
        cm = gameObject.GetComponent<ComboManager>();
        fblb = FireBaseLeaderboard.Instance;
    }

    public void TakeDamage(float _damage){
        if(allowDmg){
        if (currentHealth > 0){
            currentHealth = Mathf.Clamp(currentHealth - _damage,0 , startingHealth);
            StartCoroutine(Invulnerability());
            if(currentHealth <=0){
                fblb.addDeaths(1);
                fblb.changeScore(-100);
                resetHealth(.3f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                transform.position = Checkpoint.lastTouched[SceneManager.GetActiveScene().buildIndex].position;
                //Physics2D.IgnoreLayerCollision(0,6,false);
            }
        }
        else{
            
        }
        }
    }
    public void resetHealth(float percent){
        currentHealth = startingHealth*percent;
        cm.resetHitCount();
    }
    //testing
    void Update(){
       // if(Input.GetKeyDown(KeyCode.B)){
        //Debug.Log("It works?");
        //XPManager.instance.AddXP(20);
       // }
    }

    private IEnumerator Invulnerability(){
        //Physics2D.IgnoreLayerCollision(0,6,true);
        allowDmg = false;
        yield return new WaitForSeconds(iFrameTime);
        //Physics2D.IgnoreLayerCollision(0,6,false);
        allowDmg = true;
    }
}
