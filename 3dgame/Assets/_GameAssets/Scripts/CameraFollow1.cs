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
        // Başlangıçta offset ve rotasyon hesapla
        RecalculateOffsetAndAngles();
        lookPoint = player.position + Vector3.up * 1.5f;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            FindPlayer();
            if (player == null) return;
        }

        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 desiredPosition = player.position + rotation * offset;
        transform.position = desiredPosition;

        lookPoint = player.position + Vector3.up * 1.5f;
        transform.LookAt(lookPoint);
    }

    public void FindPlayer()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }

    // Offset ve kamera açısı hesaplama
    public void RecalculateOffsetAndAngles()
    {
        if (player != null && mainCamera != null)
        {
            offset = mainCamera.transform.position - player.transform.position;
            // Kamera rotasyonunu world -> local olarak çözümle
            Vector3 angles = mainCamera.transform.rotation.eulerAngles;
            yaw = angles.y;
            pitch = angles.x;
        }
    }

    public void RecalculateOffset()
    {
        if (player != null && mainCamera != null)
            offset = mainCamera.transform.position - player.transform.position;
    }
}