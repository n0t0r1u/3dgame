using UnityEngine;
using UnityEngine.UI;

public class DeathUIManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject deathPanel; // DeathOverlayPanel'i buraya ata
    public Button retryButton;
    public Button mainMenuButton;

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
        // Sahneyi yeniden yükle (örn. UnityEngine.SceneManagement.SceneManager.LoadScene)
    }

    void OnMainMenuClicked()
    {
        // Ana menüye dön kodunu yaz
    }
}