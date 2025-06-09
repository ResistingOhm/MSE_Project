using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    public enum BoxRewardType { None, SmallPotion, LargePotion }

    [Range(0f, 1f)] public float noneChance = 0.5f;
    [Range(0f, 1f)] public float smallPotionChance = 0.35f;
    [Range(0f, 1f)] public float largePotionChance = 0.15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();
        if (player == null) return;

       
        NetworkManager.apiManager.GetRandomJoke(1);

        string effectString;

        BoxRewardType reward = GetReward(player.stat.luck);
        switch (reward)
        {
            case BoxRewardType.SmallPotion:
                player.stat.Heal(15f);
                effectString = "Low Potion! HP +15";
                break;

            case BoxRewardType.LargePotion:
                player.stat.Heal(30f);
                effectString = "High Potion! HP +30";
                break;

            case BoxRewardType.None:
            default:
                effectString = "No Luck!";
                break;
        }

        RandomJokePrinter.rjp.SetText(2, effectString);
        RandomJokePrinter.rjp.PrinterOn();

        gameObject.SetActive(false);
    }

    private BoxRewardType GetReward(float luck)
    {
        float roll = Random.Range(0f, 1f);

        float adjustedNoneChance = Mathf.Clamp01(noneChance - luck * 0.01f);

        if (roll < adjustedNoneChance)
            return BoxRewardType.None;
        else if (roll < adjustedNoneChance + smallPotionChance)
            return BoxRewardType.SmallPotion;
        else
            return BoxRewardType.LargePotion;
    }
}
