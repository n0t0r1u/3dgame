////using UnityEngine;

////public class PlayerMove : MonoBehaviour
////{
////    public Transform cameraTransform;
////    public float moveSpeed = 5.0f;

////    // Start is called once before the first execution of Update after the MonoBehaviour is created
////    void Start()
////    {

////    }

////    // Update is called once per frame
////    void Update()
////    {
////        // Yatay ve dikey giriş al
////        float horizontalInput = Input.GetAxis("Horizontal");
////        float verticalInput = Input.GetAxis("Vertical");

////        // Hareket vektörünü oluştur
////        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);


////        if (moveDirection != Vector3.zero)
////        {
////            // Kameranın etkisini dikkate alarak hareket yönünü ayarla
////            if (cameraTransform != null)
////            {
////                moveDirection = cameraTransform.TransformDirection(moveDirection); // Kameraya göre yön
////                moveDirection.y = 0; // Y eksenindeki eğimi sıfırla
////            }

////            // Karakteri hareket yönüne döndür
////            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
////            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10); // Smooth rotation

////            // Eğer hareket varsa, karakteri hareket yönüne döndür
////            if (moveDirection != Vector3.zero)
////            {
////                // Kameranın etkisini dikkate alarak hareket yönünü ayarla
////                //if (cameraTransform != null)
////                //{
////                //    moveDirection = cameraTransform.TransformDirection(moveDirection);
////                //    moveDirection.y = 0; // Y eksenindeki eğimleri sıfırla
////                //}

////                // Karakteri hareket yönüne döndür
////                //Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
////                //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10); // Smooth rotation

////            }

////            // Karakteri hareket ettir
////            transform.position += moveDirection.normalized * moveSpeed * Time.deltaTime;

////            // Log
////            Debug.Log("Position: " + transform.position + " Rotation: " + transform.rotation.eulerAngles);
////        }
////    }
////}

//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    public Transform cameraTransform; // Kameranın Transform bileşeni
//    public float moveSpeed = 5.0f;    // Karakterin hareket hızı
//        void Start()
//    {

//    }

//    void Update()
//    {
//        // Yatay (A/D veya Sol/Sağ ok tuşları) ve dikey (W/S veya Yukarı/Aşağı ok tuşları) girişlerini al
//        float horizontalInput = Input.GetAxis("Horizontal");
//        float verticalInput = Input.GetAxis("Vertical");

//        // Eğer hiçbir giriş yoksa hareket etme
//        if (horizontalInput == 0 && verticalInput == 0)
//            return;

//        // Kameranın yönlerine göre hareket vektörünü oluştur
//        Vector3 forward = cameraTransform.forward; // Kameranın ileri yönü
//        Vector3 right = cameraTransform.right;     // Kameranın sağ yönü

//        // Y eksenindeki eğimi sıfırla, sadece yatay düzlemde hareket et
//        forward.y = 0f;
//        right.y = 0f;

//        // Hareket yönünü hesapla
//        Vector3 moveDirection = forward * verticalInput + right * horizontalInput;
//        moveDirection.Normalize(); // Hareket vektörünü normalize et

//        // Karakteri hareket yönüne doğru döndür
//        Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
//        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15); // Yumuşak dönüş

//        // Karakteri hareket ettir
//        transform.position += moveDirection * moveSpeed * Time.deltaTime;

//        // Debug Log
//        Debug.Log($"Position: {transform.position}, Rotation: {transform.rotation.eulerAngles}");
//    }
//}

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform cameraTransform; // Kameranın Transform bileşeni
    public float moveSpeed = 5.0f;    // Karakterin hareket hızı
    public float attackMoveMultiplier = 2.0f; // Saldırı sırasında hareket hızı çarpanı
    public float attackAutoMoveSpeed = 2.0f; // Saldırı sırasında otomatik ileri hareket hızı
    public Animator animator;         // Animator bileşeni

    private Vector3 moveDirection;    // Hareket yönü

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        bool isMoving = (horizontalInput != 0 || verticalInput != 0);

        // Saldırı var mı kontrol et (ör. sol mouse tuşu)
        bool isAttacking = Input.GetMouseButton(0);
        animator.SetBool("IsAttacking", isAttacking);

        // Hareket animasyonunu yönet
        animator.SetBool("Run", isMoving || isAttacking); // Saldırıda da Run'ı true yap!

        // Kameranın yönlerini al
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;

        // Eğer hareket tuşu varsa, oyuncu kontrolü
        if (isMoving)
        {
            moveDirection = forward * verticalInput + right * horizontalInput;
            moveDirection.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);

            float appliedMoveSpeed = isAttacking ? moveSpeed * attackMoveMultiplier : moveSpeed;
            transform.position += moveDirection * appliedMoveSpeed * Time.deltaTime;
        }
        // Hareket tuşu yok, saldırı animasyonu varsa otomatik ileri hareket
        else if (isAttacking)
        {
            moveDirection = forward; // Sadece ileriye doğru hareket
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15);

            transform.position += moveDirection * attackAutoMoveSpeed * Time.deltaTime;
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // Debug Log
        Debug.Log($"Position: {transform.position}, Rotation: {transform.rotation.eulerAngles}, IsAttacking: {isAttacking}");
    }
}