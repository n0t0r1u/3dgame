using UnityEngine;
using UnityEngine.UI;

public class DeathUIManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject deathPanel;
    public Button retryButton;
    public Button mainMenuButton;
    public GameObject player; // Oyuncu referansı

    void Start()
    {
        HideDeathPanel();
        retryButton.onClick.AddListener(OnRetryClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
    }

    public void ShowDeathPanel()
    {
        canvas.SetActive(true);
        deathPanel.SetActive(true);
        retryButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);

        if (player != null)
        {
            GameManager.Instance.lastDeathPosition = player.transform.position;
        }
    }

    public void HideDeathPanel()
    {
        canvas.SetActive(false);
        deathPanel.SetActive(false);
        retryButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
    }

    void OnRetryClicked()
    {
        RespawnPlayer();
    }

    void OnMainMenuClicked()
    {
        // Ana menüye dönmek istiyorsan buraya sahne yükleme koyabilirsin
        // SceneManager.LoadScene("MainMenu");
    }

    void RespawnPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            // 1. Pozisyonu sıfırla
            playerObj.transform.position = GameManager.Instance.lastDeathPosition;

            // 2. Canı fulle
            var health = playerObj.GetComponent<HealthSystemForDummies>();
            if (health != null)
            {
                health.ReviveWithMaximumHealth();
            }

            // 3. Animasyonu sıfırla
            var anim = playerObj.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Rebind();
                anim.Update(0f);
            }

            // 4. Hareket scriptini aktif yap
            var move = playerObj.GetComponent<PlayerMovement1>();
            if (move != null)
            {
                move.enabled = true;
                move.SetDead(false); // isDead değişkenini sıfırla
            }

            // 5. Death animasyonunu sıfırla
            var animCtrl = playerObj.GetComponent<AnimationController>();
            if (animCtrl != null)
            {
                animCtrl.isDead = false;
            }

            // 6. UI panelini kapat
            HideDeathPanel();
        }
        else
        {
            Debug.LogWarning("Oyuncu bulunamadı!");
        }
    }
}