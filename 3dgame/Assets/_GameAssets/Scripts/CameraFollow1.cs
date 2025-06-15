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
    public Transform player; // Bağlı olduğu karakter
    public Vector3 offset; // Kamera konumunu karaktere göre kaydırma
    public float mouseSensitivity = 100f; // Fare hassasiyeti
    public float pitchMin = -30f; // Minimum dikey açı
    public float pitchMax = 60f; // Maksimum dikey açı
    public Camera mainCamera;

    private float pitch = 0f; // Yukarı-aşağı dönüş açısı
    private float yaw = 0f; // Sağ-sol dönüş açısı

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Fare imlecini kilitle
        offset = mainCamera.transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Fare hareketlerini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Kamerayı döndür (Yaw ve Pitch)
        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        // Kameranın karakter etrafında dönmesini sağla
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = player.position + rotation * offset;

        // Kamerayı karaktere döndür
        transform.LookAt(player.position + Vector3.up * 1.5f); // Karakterin üst kısmına bakar
    }
}


