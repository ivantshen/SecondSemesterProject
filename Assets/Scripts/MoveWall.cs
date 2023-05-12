using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MoveWall : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject boss;
    public GameObject player;
    private CapsuleCollider2D cd;
    private CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CapsuleCollider2D>();
        vcam = CameraPersistence.Instance.transform.GetChild(1).gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            rb.gravityScale = 10;
            boss.SendMessage("BossTransformation");
            Destroy(cd); // turn off the collider
            StartCoroutine(increaseLensSize());
            player.SendMessage("FreezeInputs", 3f);
        }
        
    }
    private IEnumerator increaseLensSize(){
        while(vcam.m_Lens.OrthographicSize<15){
            vcam.m_Lens.OrthographicSize +=0.1f;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
