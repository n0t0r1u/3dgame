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

public class PlayerMovement1 : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 4.0f;
    public Animator animator;

    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool isMoving = (horizontalInput != 0 || verticalInput != 0);

        animator.SetBool("Run", isMoving);

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f;
        right.y = 0f;

        if (isMoving)
        {
            moveDirection = (forward * verticalInput + right * horizontalInput).normalized;
        }
        else
        {
            moveDirection = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        // Hareketi fizik güncellemesinde uygula
        if (moveDirection != Vector3.zero)
        {
            rb.linearVelocity = moveDirection * moveSpeed + new Vector3(0, rb.linearVelocity.y, 0);
        }
        else
        {
            // Tuşlar bırakıldıysa velocity'yi anında sıfırla (yukarıdan düşmeyi engellemeyecek şekilde)
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}