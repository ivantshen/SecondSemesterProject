using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCrumble : MonoBehaviour
{
    public GameObject floor;
    [SerializeField] private AudioSource breakSoundEffect;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            StartCoroutine(crumbleMyFloor());
        }
    }

    IEnumerator crumbleMyFloor() {
        yield return new WaitForSeconds(0.2f);
        breakSoundEffect.Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(floor);
    }
    
}
