using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LeaderboardCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text leaderboardNameText;
    [SerializeField] private TMP_Text leaderboardScoreText;
    [SerializeField] private TMP_Text leaderboardDeathText;
    [SerializeField] private TMP_Text leaderboardYoshiText;
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
        leaderboardNameText.text = "";
        leaderboardScoreText.text = "";
        leaderboardDeathText.text = "";
        leaderboardYoshiText.text = "";
        timer = refreshCooldown;
        FireBaseLeaderboard.Instance.displayTop(this);
    }
    public void setLeaderboardText(string name, string score, string death, string yoshi){
        leaderboardNameText.text = name;
        leaderboardScoreText.text = score;
        leaderboardDeathText.text = death;
        leaderboardYoshiText.text = yoshi;
    }
    void Update(){
        if(timer>0){
            timer-= Time.deltaTime;
        }else{
            refreshButton.interactable = true;
        }
    }
}
