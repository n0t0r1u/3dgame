using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        // Oyuncu öldüğü konumu kaydet
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
        // Sahneyi yeniden yükle
        SceneManager.sceneLoaded += RespawnAtDeathPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnMainMenuClicked()
    {
        // Ana menüye özel spawn noktası ayarla
        SceneManager.sceneLoaded += RespawnAtMainMenuPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RespawnAtDeathPosition(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = GameManager.Instance.lastDeathPosition;
        }
        SceneManager.sceneLoaded -= RespawnAtDeathPosition;
    }

    void RespawnAtMainMenuPosition(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.transform.position = GameManager.Instance.mainMenuSpawnPosition;
        }
        SceneManager.sceneLoaded -= RespawnAtMainMenuPosition;
    }
}