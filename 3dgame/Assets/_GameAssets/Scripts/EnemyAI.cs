using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform playerTransform;
    public float followDistance = 10f;  // Takip başlama mesafesi
    public float attackDistance = 2f;   // Saldırı başlama mesafesi
    public float moveSpeed = 3f;
    public Animator animator;            // Saldırı animasyonu için

    private bool isFollowing = false;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerTransform == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance < attackDistance)
        {
            // Saldırı mesafesinde
            isFollowing = false;
            animator.SetBool("IsAttacking", true);
            animator.SetBool("Run", false);
            // Burada saldırı kodun varsa ekle
        }
        else if (distance < followDistance)
        {
            // Takip et
            isFollowing = true;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("Run", true);

            // Yönünü oyuncuya çevir
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            direction.y = 0; // Dikey eksende dönmesin
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

            // Oyuncuya doğru yürü
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Oyuncu uzakta, düşman durur
            isFollowing = false;
            animator.SetBool("IsAttacking", false);
            animator.SetBool("Run", false);
        }
    }
}