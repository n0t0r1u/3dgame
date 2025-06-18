using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float respawnDelay = 10f;

    private GameObject currentEnemy;

    // Son düşman durumu
    private Vector3 lastPosition;
    private Quaternion lastRotation;
    private float lastHealth;
    private bool hasLastState = false;

    void Start()
    {
        // İlk spawn spawner'ın pozisyonunda ve full canla olsun
        lastPosition = transform.position;
        lastRotation = transform.rotation;
        lastHealth = 1000f; // Prefab'ın varsayılan canı
        hasLastState = false;
        SpawnEnemy();
    }

    // Düşman öldüğünde çağrılır
    public void StartRespawn()
    {
        Invoke(nameof(SpawnEnemy), respawnDelay);
    }

    // Düşmanın ölmeden hemen önceki state'ini kaydeder
    public void SaveEnemyState(EnemyAI ai)
    {
        lastPosition = ai.transform.position;
        lastRotation = ai.transform.rotation;
        var health = ai.GetComponent<HealthSystemForDummies>();
        if (health != null)
            lastHealth = health.CurrentHealth;
        hasLastState = true;
    }

    void SpawnEnemy()
    {
        Vector3 spawnPos = hasLastState ? lastPosition : transform.position;
        Quaternion spawnRot = hasLastState ? lastRotation : transform.rotation;

        currentEnemy = Instantiate(enemyPrefab, spawnPos, spawnRot);

        // Canı ve spawner referansını ata
        
    }
}