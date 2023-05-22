using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class YoshiCollectible : MonoBehaviour
{
    [SerializeField] private AudioSource yoshiSoundEffect;
    private bool pickedUp = false;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (!pickedUp&&collision.gameObject.tag == "Player") {
            pickedUp = true;
            StartCoroutine(yoshiPickup());
        }
    }
    IEnumerator yoshiPickup() {
        yoshiSoundEffect.Play();
        yield return new WaitForSeconds(0.7f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        ScenePersist.scenes[sceneIndex].setPickedUp(gameObject);
        FireBaseLeaderboard.Instance.increaseYoshiCount(1);
        FireBaseLeaderboard.Instance.changeScore(150);
        Destroy(gameObject);
    }
}
