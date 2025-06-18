
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private KeyCode[] movementKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    private int comboIndex = 0;
    private float attackTimer = 0f;
    private float comboCooldown = 0.5f; // animasyonlar arası süre

    private bool isDead = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator == null || isDead) return;

        // Hareket kontrolü
        bool isMoving = false;
        foreach (KeyCode key in movementKeys)
        {
            if (Input.GetKey(key))
            {
                isMoving = true;
                break;
            }
        }
        animator.SetBool("Run", isMoving);
        
        if (Input.GetMouseButton(0))
        {
            isMoving = true;
        }
        animator.SetBool("Run", isMoving);

        // Saldırı kontrolü (basılı tutulunca)
        bool isAttacking = Input.GetKey(KeyCode.Space);
        animator.SetBool("IsAttacking", isAttacking);

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= comboCooldown)
            {
                attackTimer = 0f;
                comboIndex = (comboIndex + 1) % 5; // 0–4arası döner
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

    // Ölüm animasyonunu tetikleyen fonksiyon
    public void PlayDeathAnimation()
    {
        if (isDead) return;
        isDead = true;
        animator.SetTrigger("Die");
    }
}