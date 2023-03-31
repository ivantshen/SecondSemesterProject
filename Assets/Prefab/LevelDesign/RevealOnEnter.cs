using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RevealOnEnter : MonoBehaviour
{
    [SerializeField] private GameObject[] thingsToReveal;
    [SerializeField] private float[] opacityPerSecond;
    [SerializeField] private float[] revealDelays;
    private SpriteRenderer[] thingSprites;
    private bool reveal = false;
    [SerializeField] private bool revealForever = false;
    [SerializeField] private float destroyTimer;
    void Start(){
        thingSprites = new SpriteRenderer[thingsToReveal.Length];
        for(int i=0;i<thingsToReveal.Length;i++){
            thingSprites[i] = thingsToReveal[i].GetComponent<SpriteRenderer>();
        }
    }
    void Update(){
        if(reveal){
            for (int i=0;i<thingSprites.Length;i++){
                if(revealDelays[i]<=0){
                Color c = thingSprites[i].color;
                thingSprites[i].color = new Color(c.r,c.b,c.g,c.a-=(Time.deltaTime*opacityPerSecond[i]));    
                }else{
                    revealDelays[i]-=Time.deltaTime;
                }
            }
            destroyTimer-=Time.deltaTime;
            if(destroyTimer<0){
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
        reveal = true;    
        }
        if(revealForever){
            for(int i=0;i<thingsToReveal.Length;i++){
                if(thingsToReveal[i].tag=="Nonpermanent"){
                    ScenePersist.scenes[SceneManager.GetActiveScene().buildIndex].setPickedUp(thingsToReveal[i]);
                }
            }
        }
    }
}
