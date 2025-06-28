using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private KeyCode[] movementKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    private int comboIndex = 0;
    private float attackTimer = 0f;
    private float comboCooldown = 0.5f; // animasyonlar arası süre

    public bool isDead = false;

    [HideInInspector] public bool isAttacking = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (animator == null || isDead) return;

        // Hareket kontrolü
  
        // Saldırı kontrolü
        isAttacking = Input.GetKey(KeyCode.Space);
        animator.SetBool("IsAttacking", isAttacking);

        if (isAttacking)
        {
            animator.SetBool("Run", false); // Saldırı sırasında koşma kapalı
            attackTimer += Time.deltaTime;
            if (attackTimer >= comboCooldown)
            {
                attackTimer = 0f;
                comboIndex = (comboIndex + 1) % 5;
                animator.SetInteger("ComboIndex", comboIndex);
            }
        }
        else
        {
            
            comboIndex = 0;
            attackTimer = 0f;
            animator.SetInteger("ComboIndex", comboIndex);
        }
    }

    public void PlayDeathAnimation()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("Die");
    }
}