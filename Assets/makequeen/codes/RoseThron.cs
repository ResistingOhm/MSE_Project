using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseThorn : MonoBehaviour
{
    public float damage = 3f;
    public float radius = 2f;
    public float duration = 1.5f;
    public float poisonDuration = 3f;

    private float tickInterval = 0.5f;
    private float tickTimer = 0f;

    void OnEnable()
    {
        StartCoroutine(DeactivateAfterDuration());
        tickTimer = 0f;
    }

    void Update()
    {
        tickTimer += Time.deltaTime;

        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            ApplyDamageAndPoison();
        }
    }

    void ApplyDamageAndPoison()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Enemy"));
        foreach (Collider2D col in enemies)
        {
            Enemy enemy = col.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.PoisonDamage(poisonDuration);

                Player player = FindObjectOfType<Player>();
                if (player != null)
                {
                    AudioSource audioSource = player.GetComponent<AudioSource>();
                    if (audioSource != null && player.PhitSFX.Length > 0)
                    {
                        audioSource.PlayOneShot(player.PhitSFX[Random.Range(0, player.PhitSFX.Length)]);
                    }
                }
                
            }
        }
    }

    IEnumerator DeactivateAfterDuration()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
