using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public Button hpButton;
    public Button attackButton;
    public Button defenseButton;
    public Button luckButton;
    public Button speedButton;

    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>(); 

        hpButton.onClick.AddListener(() => player.IncreaseStat(1));
        attackButton.onClick.AddListener(() => player.IncreaseStat(2));
        defenseButton.onClick.AddListener(() => player.IncreaseStat(3));
        luckButton.onClick.AddListener(() => player.IncreaseStat(4));
        speedButton.onClick.AddListener(() => player.IncreaseStat(5));
    }
}
