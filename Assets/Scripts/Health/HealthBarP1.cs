using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarP1 : MonoBehaviour
{
    [SerializeField] private HealthP1 playerHealth;
    [SerializeField] private Image totalhealthBar;
    [SerializeField] private Image currenthealthBar;

    private void Start(){
        totalhealthBar.fillAmount = playerHealth.currentHealth / 100;
    }

    private void Update(){
        currenthealthBar.fillAmount = playerHealth.currentHealth / 100;
    }
}
