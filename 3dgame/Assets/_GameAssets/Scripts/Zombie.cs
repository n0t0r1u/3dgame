using UnityEngine;

public class Zombie : MonoBehaviour
{
    void Die()
    {
        // Player’a referans bul (ör: yakınındaki oyuncu, ya da GameManager üzerinden)
        PlayerQuestSystem playerQuestSystem = FindObjectOfType<PlayerQuestSystem>();
        if (playerQuestSystem != null)
        {
            playerQuestSystem.OnZombieKilled();
        }
        Destroy(gameObject);
    }
}