using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordSummonUlt : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private Vector3 spawnLocationOffset;
    [SerializeField] private float rotation;
    [SerializeField] private int hitCost;
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    [SerializeField] private int deathTime;
    [SerializeField] private bool multiTarget;
    [SerializeField] private bool wallClipping;
    [SerializeField] private bool contactDamage;
    private ComboManager cm;
    private GameObject player;
    void Start(){
        player = PlayerPersistence.Instance;
        cm = PlayerPersistence.Instance.GetComponent<ComboManager>();
    }
    public void SwordUlt(InputAction.CallbackContext context){
        if(context.performed&&cm.getHitCount()>=hitCost){
            cm.decreaseHitCount(hitCost);
            PlayerBulletScript s = null;
            if(player.transform.rotation.y<0){
            s = Instantiate(sword,transform.position+new Vector3(-spawnLocationOffset.x,spawnLocationOffset.y,0),Quaternion.Euler(0,0,180-rotation)).GetComponent<PlayerBulletScript>();
            }else{
            s = Instantiate(sword,transform.position+spawnLocationOffset,Quaternion.Euler(0,0,rotation)).GetComponent<PlayerBulletScript>();
            }
            s.setStats(speed,damage,deathTime,multiTarget,wallClipping,contactDamage);
        }
    }
}
