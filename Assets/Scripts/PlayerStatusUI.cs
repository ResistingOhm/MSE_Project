using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusUI : MonoBehaviour
{
    
    public Slider HpBarSlider;
    public Slider ExpBarSlider;
    public Text levelText;

    public PlayerStat playerStat;
    void Awake()
    {
        playerStat = GetComponent<PlayerStat>();
    }
    public void UpdateHPbar(){
        if(playerStat != null && HpBarSlider != null){
            HpBarSlider.value = playerStat.currentHP / playerStat.maxHP;
        }
    }
    public void UpdateExpbar(){
        if (playerStat != null && ExpBarSlider != null) {
            ExpBarSlider.value = playerStat.currentExp / playerStat.maxExp;
        }
    }
    public void UpdateLevel()
    {
        if (levelText != null)
        {
            levelText.text = "Lv. " + playerStat.level;
        }
    }
    void Update()
    {
        UpdateHPbar();
        UpdateExpbar();
        UpdateLevel();
    }
}
