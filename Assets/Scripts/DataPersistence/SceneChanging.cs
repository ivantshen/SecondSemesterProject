using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanging : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private Vector3 travelLocation;
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="Player"){
            SceneManager.LoadScene(sceneIndex);
            PlayerPersistence.Instance.transform.position = travelLocation;
        }
    }
}
