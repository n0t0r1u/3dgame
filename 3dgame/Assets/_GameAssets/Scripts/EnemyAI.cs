using UnityEngine;
using System.Collections;

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
    public EnemySpawner spawner;

    public Vector3 patrolCenter;
    public float patrolRadius = 5f;

    private Vector3 patrolTarget;
    private float patrolTimer = 0f;
    private float movingRandomDuration = 3f;
    private float waitDuration = 1f;

    private HealthSystemForDummies healthSystem;
    private bool isDead = false; 

    private HealthSystemForDummies playerHealth;

    private float respawnDelay = 10f;
    private float disappearDelay = 10f;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        patrolCenter = transform.position;
        SetNewPatrolTarget();
        patrolState = PatrolState.MovingRandom;
        patrolTimer = 0f;

        healthSystem = GetComponent<HealthSystemForDummies>();
        if (playerTransform != null)
        {
            playerHealth = playerTransform.GetComponent<HealthSystemForDummies>();
        }

        // Health event dinle
        if (healthSystem != null)
        {
            healthSystem.OnIsAliveChanged.AddListener(OnIsAliveChanged);
        }
    }

    private void OnIsAliveChanged(bool isAlive)
    {
        if (!isAlive && !isDead)
        {
            Die();
        }
    }

    void Update()
    {
        if (isDead) return;

        bool playerIsAlive = true;
        if (playerHealth != null)
            playerIsAlive = playerHealth.IsAlive;

        if (!playerIsAlive)
        {
            animator.SetBool("IsAttacking", false);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);

            if (patrolState != PatrolState.MovingRandom && patrolState != PatrolState.WaitingAfterRandom &&
                patrolState != PatrolState.ReturningCenter && patrolState != PatrolState.WaitingAtCenter)
            {
                patrolState = PatrolState.MovingRandom;
                patrolTimer = 0f;
                SetNewPatrolTarget();
            }

            Patrol();
            return;
        }

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

        Patrol();
    }

    void Die()
    {
        isDead = true;
        if (spawner != null)
            spawner.SaveEnemyState(this);
            spawner.StartRespawn();

        if (animator != null)
        {
            animator.SetTrigger("Die");
            animator.SetBool("IsAttacking", false);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }

        // Hasar veren collider ve scriptlerini devre dışı bırak
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = false;

        foreach (var atk in GetComponentsInChildren<EnemyAttack>())
            atk.SetDead(true);

        StartCoroutine(DisappearAndRespawnRoutine());
        Destroy(gameObject, 3f);
    }

    IEnumerator DisappearAndRespawnRoutine()
    {
        // 10 saniye sonra yok et
        yield return new WaitForSeconds(disappearDelay);
        gameObject.SetActive(false);

        // 10 saniye yok kal, sonra tekrar doğ
        yield return new WaitForSeconds(respawnDelay);

        Respawn();
    }

    void Respawn()
    {
        // Tam canlı olarak tekrar doğ
        isDead = false;
        gameObject.SetActive(true);

        // Collider ve scriptleri tekrar aç
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = true;

        foreach (var atk in GetComponentsInChildren<EnemyAttack>())
            atk.SetDead(false);

        // Sağlığı max yap
        if (healthSystem != null)
        {
            healthSystem.ReviveWithMaximumHealth();
        }

        // Konum ve AI reset
        transform.position = patrolCenter;
        SetNewPatrolTarget();
        patrolState = PatrolState.MovingRandom;
        patrolTimer = 0f;

        // Animasyonları resetle
        if (animator != null)
        {
            animator.Rebind();
            animator.Update(0f);
            animator.SetBool("IsAttacking", false);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
    }

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
                }
                break;

            case PatrolState.WaitingAfterRandom:
                animator.SetBool("Walk", false);
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= waitDuration)
                {
                    patrolTimer = 0f;
                    patrolState = PatrolState.ReturningCenter;
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