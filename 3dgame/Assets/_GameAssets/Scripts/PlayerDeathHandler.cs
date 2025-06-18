using UnityEngine;

public class PlayerDeathHandler : MonoBehaviour
{
    private HealthSystemForDummies healthSystem;
    private PlayerMovement1 playerMovement;
    private AnimationController animationController;

    void Start()
    {
        healthSystem = GetComponent<HealthSystemForDummies>();
        animationController = GetComponent<AnimationController>();
        playerMovement = GetComponent<PlayerMovement1>();

        if (healthSystem != null)
        {
            healthSystem.OnIsAliveChanged.AddListener(OnIsAliveChanged);
        }
    }

    private void OnIsAliveChanged(bool isAlive)
    {
        // Ölüm animasyonu tetikle
        if (!isAlive && animationController != null)
        {
            animationController.PlayDeathAnimation();
        }

        // Hareket scriptini devre dışı bırak/aktif et
        if (playerMovement != null)
        {
            playerMovement.enabled = isAlive;
        }
    }
}