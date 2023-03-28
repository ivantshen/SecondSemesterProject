using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordSummonUlt : MonoBehaviour
{
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject swordPortal;
    [SerializeField] private Vector2 spawnLocation;
    [SerializeField] private Vector2 spawnLocationOffset;
    [SerializeField] private float rotation;
    [SerializeField] private float rotationOffset;
    [SerializeField] private int minHitCost;
    [SerializeField] private int speed;
    [SerializeField] private int damage;
    [SerializeField] private int deathTime;
    [SerializeField] private bool multiTarget;
    [SerializeField] private bool wallClipping;
    [SerializeField] private bool contactDamage;
    [SerializeField] private float summonDuration;
    private bool allowAbility = true;
    private ComboManager cm;
    private GameObject player;
    void Start(){
        player = PlayerPersistence.Instance;
        cm = PlayerPersistence.Instance.GetComponent<ComboManager>();
    }
    public void SwordUlt(InputAction.CallbackContext context){
        if(context.performed&&cm.getHitCount()>=(minHitCost*2)&&allowAbility){
            allowAbility = false;
            bool facingRight;
            if(player.transform.rotation.y<0){
                facingRight = false;
            }else{
                facingRight = true;
            }
            StartCoroutine(swordRain(facingRight));
        }
    }
    private IEnumerator swordRain(bool facingRight){
        cm.allowCombo(false);
        Vector2 playerPos = player.transform.position;
        float rot = rotation;
        if(!facingRight){
            rot = 180-rotation;
            Instantiate(swordPortal,playerPos-new Vector2(spawnLocation.x,-spawnLocation.y),Quaternion.identity,null);
        }else{
            rot = rotation;
            Instantiate(swordPortal,playerPos+spawnLocation,Quaternion.identity,null);
        }
        int numSwords = (cm.getHitCount()/minHitCost)/2; //half the number of swords that can be summoned
        Debug.Log(numSwords);
        for(int i=0;i<numSwords;i++){
            PlayerBulletScript s = Instantiate(sword,randomSpawnLocation(facingRight,playerPos),Quaternion.Euler(0,0,rot+Random.Range(-rotationOffset,rotationOffset))).GetComponent<PlayerBulletScript>();
            s.setStats(speed,damage,deathTime,multiTarget,wallClipping,contactDamage);
            cm.decreaseHitCount(minHitCost);
            yield return new WaitForSeconds(summonDuration/numSwords);
        }
        allowAbility = true;
        cm.allowCombo(true);
    }
    private Vector2 randomSpawnLocation(bool facingRight,Vector2 localPos){
        Vector2 spawn;
        if(!facingRight){
        spawn = new Vector2(localPos.x-spawnLocation.x+Random.Range(-spawnLocationOffset.x,spawnLocationOffset.x),localPos.y+spawnLocation.y+Random.Range(-spawnLocationOffset.y,spawnLocationOffset.y));
        }else{
        spawn = new Vector2(localPos.x+spawnLocation.x +Random.Range(-spawnLocationOffset.x,spawnLocationOffset.x),localPos.y+spawnLocation.y+Random.Range(-spawnLocationOffset.y,spawnLocationOffset.y));    
        }
        return spawn;
    }
}
