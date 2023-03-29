using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenePersist : MonoBehaviour
{
    private int sceneIndex;
    [SerializeField] private GameObject[] oneTimePickups;
    private string[] pickUpNames;
    [SerializeField] private bool[] pickedUpItems;
    public static ScenePersist[] scenes;
    void Awake(){
        if(scenes==null||scenes.Length==0){
            scenes = new ScenePersist[SceneManager.sceneCountInBuildSettings];
        }
        oneTimePickups =  GameObject.FindGameObjectsWithTag("Nonpermanent");
        pickedUpItems = new bool[oneTimePickups.Length];
        pickUpNames = new string[oneTimePickups.Length];
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        for(int i=0;i<scenes.Length;i++){
            if(scenes[i]&&sceneIndex==i){
                Destroy(gameObject);
                return;
            }
        }
        for(int i=0;i<oneTimePickups.Length;i++){
            pickUpNames[i] = oneTimePickups[i].name;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        scenes[sceneIndex] = this;
        DontDestroyOnLoad(this);
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex==sceneIndex){
            updatePickUps();
            for(int j=0;j<pickedUpItems.Length;j++){
                if(pickedUpItems[j]){
                Destroy(oneTimePickups[j]);
                }
            }
        }
    }
        
    public GameObject[] getPickups(){
        return oneTimePickups;
    }
    public bool[] getPickupStatus(){
        return pickedUpItems;
    }
    public void updatePickUps(){
        GameObject[] currentPickups = GameObject.FindGameObjectsWithTag("Nonpermanent");
        for(int i=0;i<oneTimePickups.Length;i++){
            for(int j=0;j<currentPickups.Length;j++){
                if(currentPickups[j].name==pickUpNames[i]){
                    oneTimePickups[i] = currentPickups[j];
                }
            }
        }
    }
    public void setPickedUp(GameObject g){
        for(int i=0;i<pickedUpItems.Length;i++){
            if(g==oneTimePickups[i]){
                pickedUpItems[i] = true;
            }
        }
    }
}
