using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 50f;
    public float attackCooldown = 2.633f; // Saniye cinsinden bekleme süresi

    private float lastAttackTime = -Mathf.Infinity;
    private HealthSystemForDummies playerHealth;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealth == null)
                playerHealth = collision.gameObject.GetComponent<HealthSystemForDummies>();

            if (playerHealth != null && Time.time - lastAttackTime >= attackCooldown)
            {
                playerHealth.AddToCurrentHealth(-damage);
                lastAttackTime = Time.time;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth = null; // Temas bittiğinde referansı temizle
        }
    }
}