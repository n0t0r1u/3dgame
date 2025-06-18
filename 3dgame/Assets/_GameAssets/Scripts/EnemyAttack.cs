/*using UnityEngine;

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
}*/
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int minDamage = 10;
    public int maxDamage = 25;
    private bool isDead = false;

    public void SetDead(bool dead)
    {
        isDead = dead;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            HealthSystemForDummies playerHealth = other.GetComponent<HealthSystemForDummies>();
            if (playerHealth != null)
            {
                int randomDamage = Random.Range(minDamage, maxDamage + 1);
                playerHealth.AddToCurrentHealth(-randomDamage);
            }
        }
    }
}