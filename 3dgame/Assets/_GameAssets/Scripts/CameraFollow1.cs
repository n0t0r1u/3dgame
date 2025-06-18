//using UnityEngine;

//public class CameraFallow : MonoBehaviour
//{

//    public Transform player;// Oyuncunun transform'u
//    public Vector3 offset;
//    public Camera camera;// Kameranın oyuncuya olan uzaklığı
//    private void Start()
//    {
//        offset = camera.transform.position- player.transform.position ;
//    }
//    void LateUpdate()
//    {

//        // Kamerayı oyuncuya göre hareket ettir
//        transform.position = player.position + offset;
//    }
//}
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
        offset = mainCamera.transform.position - player.transform.position;
        lookPoint = player.position + Vector3.up * 1.5f;
    }

    void LateUpdate()
    {
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
}