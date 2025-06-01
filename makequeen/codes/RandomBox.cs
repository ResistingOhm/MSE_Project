using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBox : MonoBehaviour
{
    private bool used = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (used || !collision.CompareTag("Player"))
            return;

        used = true;

        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            GiveRandomReward(player);
        }

        Destroy(gameObject);
    }

    void GiveRandomReward(Player player)
    {
        float badChance = 0.6f - player.stat.luck * 0.01f;
        float midChance = 0.3f + player.stat.luck * 0.007f;
        float goodChance = 1f - badChance - midChance;

        float roll = Random.value;

        if (roll < badChance)
        {
            Debug.Log("랜덤박스: 꽝!");
        }
        else if (roll < badChance + midChance)
        {
            player.stat.Heal(15);
            Debug.Log("랜덤박스: 하급 포션 체력 +15");
        }
        else
        {
            player.stat.Heal(30);
            Debug.Log("랜덤박스: 상급 포션 체력 +30");
        }
    }
}
