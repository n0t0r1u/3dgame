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
        SceneManager.sceneLoaded += RespawnAtDeathPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnMainMenuClicked()
    {
        SceneManager.sceneLoaded += RespawnAtMainMenuPosition;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void RespawnAtDeathPosition(Scene scene, LoadSceneMode mode)
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerObj.transform.position = GameManager.Instance.lastDeathPosition;
            player = playerObj;

            // Kamera scriptini bul ve yeni player'ı ata
            CameraFollow1 camFollow = FindObjectOfType<CameraFollow1>();
            if (camFollow != null)
                camFollow.SetPlayer(player.transform);
        }
        SceneManager.sceneLoaded -= RespawnAtDeathPosition;
    }

    void RespawnAtMainMenuPosition(Scene scene, LoadSceneMode mode)
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            playerObj.transform.position = GameManager.Instance.mainMenuSpawnPosition;
            player = playerObj;

            CameraFollow1 camFollow = FindObjectOfType<CameraFollow1>();
            if (camFollow != null)
                camFollow.SetPlayer(player.transform);
        }
        SceneManager.sceneLoaded -= RespawnAtMainMenuPosition;
    }
}