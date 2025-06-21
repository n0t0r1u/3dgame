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
    public Rigidbody rb; // Inspector'dan atayacaksın, eğer Rigidbody yoksa ekle
    public GameObject healthBarObject; // Inspector'dan atayacaksın

    public Vector3 patrolCenter;
    public float patrolRadius = 5f;

    private Vector3 patrolTarget;
    private float patrolTimer = 0f;
    private float movingRandomDuration = 3f;
    private float waitDuration = 1f;

    private HealthSystemForDummies healthSystem;
    private bool isDead = false;

    private HealthSystemForDummies playerHealth;

    public float disappearDelay = 5f; // 5 saniye sonra görünmez
    public float respawnDelay = 10f;  // Görünmezken 10 saniye bekle

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
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        Debug.Log("Düşman öldü: " + gameObject.name);


        if (animator != null)
        {
            animator.SetTrigger("Die");
            animator.SetBool("IsAttacking", false);
            animator.SetBool("Run", false);
            animator.SetBool("Walk", false);
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
    if (player != null)
    {
        Debug.Log("Player bulundu: " + player.name);
        PlayerQuestSystem questSystem = player.GetComponent<PlayerQuestSystem>();
        if (questSystem != null)
        {
            Debug.Log("PlayerQuestSystem bulundu, ZombieKilled çağrılıyor");
            questSystem.OnZombieKilled();
        }
        else
        {
            Debug.Log("PlayerQuestSystem bulunamadı!");
        }
    }
    else
    {
        Debug.Log("Player bulunamadı!");
    }

        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = false;

        foreach (var atk in GetComponentsInChildren<EnemyAttack>())
            atk.SetDead(true);

        StartCoroutine(DisappearAndRespawnRoutine());
    }
    void SetVisible(bool visible)
    {
        // MeshRenderer'ları aç/kapat
        foreach (var renderer in GetComponentsInChildren<Renderer>())
            renderer.enabled = visible;
        // İstersen Collider'lar da buraya eklenebilir
        if (healthBarObject != null)
        {
            healthBarObject.SetActive(visible);
        }
    }

    IEnumerator DisappearAndRespawnRoutine()
    {
        // 5 saniye sonra görünmez ol
        yield return new WaitForSeconds(disappearDelay);
         SetVisible(false);

        // Görünmezken 10 saniye bekle
        yield return new WaitForSeconds(respawnDelay);

        // Yeniden doğ
        Respawn();
    }

    void Respawn()
    {
        rb.isKinematic = false;
        // Objeyi tekrar aktif et
        SetVisible(true);

        // Sağlığı tam yap
        if (healthSystem != null)
            healthSystem.ReviveWithMaximumHealth();

        // Collider ve saldırı scriptlerini tekrar aç
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = true;
        foreach (var atk in GetComponentsInChildren<EnemyAttack>())
            atk.SetDead(false);

        // Konumu ve AI state'i resetle
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

        isDead = false;
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