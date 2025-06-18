using UnityEngine;

public class PlayerAttackTrigger : MonoBehaviour
{
    public float damageAmount = 50f;
    public Animator animator; // Inspector'dan atayabilirsin veya Start'ta alabilirsin

    private void Start()
    {
        // Eğer Inspector'da atamazsan, buradan ana objedeki Animator'u alabilirsin
        if (animator == null)
            animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Sadece saldırı animasyonu aktifse hasar ver
        if (other.CompareTag("Enemy") && animator != null && animator.GetBool("IsAttacking"))
        {
            HealthSystemForDummies enemyHealth = other.GetComponent<HealthSystemForDummies>();
            if (enemyHealth != null)
            {
                enemyHealth.AddToCurrentHealth(-damageAmount);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Gerekli değil, isteğe bağlı
    }
}