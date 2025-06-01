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
            Debug.Log("�����ڽ�: ��!");
        }
        else if (roll < badChance + midChance)
        {
            player.stat.Heal(15);
            Debug.Log("�����ڽ�: �ϱ� ���� ü�� +15");
        }
        else
        {
            player.stat.Heal(30);
            Debug.Log("�����ڽ�: ��� ���� ü�� +30");
        }
    }
}
