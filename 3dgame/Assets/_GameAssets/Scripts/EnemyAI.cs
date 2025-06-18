using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum PatrolState { MovingRandom, WaitingAfterRandom, ReturningCenter, WaitingAtCenter }
    public PatrolState patrolState = PatrolState.MovingRandom;

    public Transform playerTransform;
    public float followDistance = 8f;
    public float attackAnimDistance = 2f;
    public float attackDistance = 1f;
    public float moveSpeed = 4f;
    public float patrolSpeed = 2f;
    public Animator animator;

    public Vector3 patrolCenter;
    public float patrolRadius = 5f;

    private Vector3 patrolTarget;
    private float patrolTimer = 0f;
    private float movingRandomDuration = 3f;
    private float waitDuration = 1f;

    // EKLENDİ: Player health referansı
    private HealthSystemForDummies playerHealth;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        patrolCenter = transform.position;
        SetNewPatrolTarget();
        patrolState = PatrolState.MovingRandom;
        patrolTimer = 0f;

        // EKLENDİ: Player health referansını al
        if (playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<HealthSystemForDummies>();
        }
    }

    void Update()
    {
        // EKLENDİ: Player hayatta mı kontrolü
        bool playerIsAlive = true;
        if (playerHealth != null)
            playerIsAlive = playerHealth.IsAlive;

        // Oyuncu hayatta değilse, patrol moduna dön
        if (!playerIsAlive)
        {
            animator.SetBool("IsAttacking", false);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);

            // Patrol state'e dönmeyi sağla (sadece ilk kez yap)
            if (patrolState != PatrolState.MovingRandom && patrolState != PatrolState.WaitingAfterRandom &&
                patrolState != PatrolState.ReturningCenter && patrolState != PatrolState.WaitingAtCenter)
            {
                patrolState = PatrolState.MovingRandom;
                patrolTimer = 0f;
                SetNewPatrolTarget();
            }

            // Patrol davranışını devam ettir
            Patrol();
            return;
        }

        // Oyuncu hayattaysa, mevcut davranışları uygula
        if (playerTransform != null)
        {
            float playerDistance = Vector3.Distance(transform.position, playerTransform.position);

            if (playerDistance < attackAnimDistance)
            {
                animator.SetBool("IsAttacking", true);
                animator.SetBool("Run", false);
                animator.SetBool("Walk", false);
                return;
            }
            else if (playerDistance < followDistance)
            {
                animator.SetBool("IsAttacking", false);
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
                MoveTowards(playerTransform.position, moveSpeed);
                return;
            }
        }

        // PATROL MODU
        Patrol();
    }

    // DEVRIYE DAVRANIŞLARINI FONKSİYONA AYIRDIK
    void Patrol()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("Run", false);

        switch (patrolState)
        {
            case PatrolState.MovingRandom:
                animator.SetBool("Walk", true);
                MoveTowards(patrolTarget, patrolSpeed);
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= movingRandomDuration)
                {
                    patrolTimer = 0f;
                    patrolState = PatrolState.WaitingAfterRandom;
                    animator.SetBool("Walk", false);
                    Debug.Log("Patrol: Reached random target, now waiting.");
                }
                break;

            case PatrolState.WaitingAfterRandom:
                animator.SetBool("Walk", false);
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= waitDuration)
                {
                    patrolTimer = 0f;
                    patrolState = PatrolState.ReturningCenter;
                    Debug.Log("Patrol: Done waiting, returning to center.");
                }
                break;

            case PatrolState.ReturningCenter:
                animator.SetBool("Walk", true);
                MoveTowards(patrolCenter, patrolSpeed);
                if (Vector3.Distance(transform.position, patrolCenter) < 0.1f)
                {
                    transform.position = patrolCenter;
                    animator.SetBool("Walk", false);
                    patrolTimer = 0f;
                    patrolState = PatrolState.WaitingAtCenter;
                    Debug.Log("Patrol: Arrived at center, waiting at center.");
                }
                break;

            case PatrolState.WaitingAtCenter:
                animator.SetBool("Walk", false);
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= waitDuration)
                {
                    patrolTimer = 0f;
                    SetNewPatrolTarget();
                    patrolState = PatrolState.MovingRandom;
                    Debug.Log("Patrol: Selecting new random target.");
                }
                break;
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position);
        direction.y = 0;
        float distanceToTarget = direction.magnitude;

        if (distanceToTarget > 0.01f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            transform.position += direction.normalized * speed * Time.deltaTime;
        }
    }

    void SetNewPatrolTarget()
    {
        Vector2 circle = Random.insideUnitCircle.normalized * patrolRadius;
        patrolTarget = patrolCenter + new Vector3(circle.x, 0, circle.y);
    }
}