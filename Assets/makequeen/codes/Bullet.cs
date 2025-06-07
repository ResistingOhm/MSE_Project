using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 dir) {
        this.damage = damage;
        this.per = per;

        if (per > -1) {
            rigid.velocity = dir * 15f;

        }
    }

    //°üÅë
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            AudioSource audioSource = player.GetComponent<AudioSource>();
            if (audioSource != null && player.PhitSFX.Length > 0)
            {
                audioSource.PlayOneShot(player.PhitSFX[Random.Range(0, player.PhitSFX.Length)]);
            }
        }

        per--;

        if (per == -1) {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
