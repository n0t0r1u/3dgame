using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public int minDamage = 10;
    public int maxDamage = 25;
    public Animator animator; // Inspector'dan atayabilirsin veya Start'ta alabilirsin

    private void Start()
    {
        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Boss") && animator != null && animator.GetBool("IsAttacking"))
        {
            HealthSystemForDummies enemyHealth = other.GetComponent<HealthSystemForDummies>();
            if (enemyHealth != null)
            {
                int randomDamage = Random.Range(minDamage, maxDamage + 1);
                enemyHealth.AddToCurrentHealth(-randomDamage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Gerekli değil, isteğe bağlı
    }
}