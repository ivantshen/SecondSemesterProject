using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPrefab : MonoBehaviour
{
    [SerializeField] private GameObject prefab1;
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private bool random;
    [SerializeField] private float xPos;
    [SerializeField] private float yPos;



    public void OnSpawnPrefab(){
        Instantiate(prefab1, new Vector2(xPos,yPos), Quaternion.identity );

    }
}
