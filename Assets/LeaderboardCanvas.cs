using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LeaderboardCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text leaderboardText;
    [SerializeField] private Button refreshButton;
    [SerializeField] private float refreshCooldown;
    private float timer;
    void Start(){
        refreshButton.onClick.AddListener(refreshLeaderboard);
    }
    void OnEnable(){
        refreshLeaderboard();
    }
    public void refreshLeaderboard(){
        refreshButton.interactable = false;
        leaderboardText.text = "";
        timer = refreshCooldown;
        FireBaseLeaderboard.Instance.displayTop(this);
    }
    public void setLeaderboardText(string x){
        leaderboardText.text = x;
    }
    void Update(){
        if(timer>0){
            timer-= Time.deltaTime;
        }else{
            refreshButton.interactable = true;
        }
    }
}
