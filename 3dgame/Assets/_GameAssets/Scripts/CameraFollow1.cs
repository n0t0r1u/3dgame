using UnityEngine;

public class CameraFollow1 : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float mouseSensitivity = 100f;
    public float pitchMin = -30f;
    public float pitchMax = 60f;
    public Camera mainCamera;

    private float pitch = 0f;
    private float yaw = 0f;
    private Vector3 lookPoint;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (player == null)
            FindPlayer();
        offset = mainCamera.transform.position - player.transform.position;
        lookPoint = player.position + Vector3.up * 1.5f;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            FindPlayer();
            if (player == null) return;
        }

        // Sağ tuş ile kamera döndürme
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        }

        // Kamera pozisyonunu güncelle
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = desiredPosition;

        // Kamera her zaman oyuncunun üst kısmına bakar
        lookPoint = player.position + Vector3.up * 1.5f;
        transform.LookAt(lookPoint);
    }

    // Player'ı sahnede tag ile bulmak için fonksiyon
    public void FindPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    // Dışarıdan kolayca çağrılabilmesi için
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
        // Offset ve diğer parametreleri yeniden hesapla
        offset = mainCamera.transform.position - player.transform.position;
        lookPoint = player.position + Vector3.up * 1.5f;
    }
    public void RecalculateOffset()
    {
        if (player != null && mainCamera != null)
            offset = mainCamera.transform.position - player.transform.position;
    }
}