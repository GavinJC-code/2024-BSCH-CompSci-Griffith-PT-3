using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FG_HealthBar : MonoBehaviour
{
    [SerializeField] private FG_Health health;
    [SerializeField] private Image totalHealthBar;
    [SerializeField] private Image currentHealthBar;

    private void Start(){
        totalHealthBar.fillAmount = health.currentHealth / 10;
    }
    private void Update(){
        currentHealthBar.fillAmount = health.currentHealth / 10;
    }
}
