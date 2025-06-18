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
    private bool isDead = false;

    // Mouse tıklama ile hareket için
    private bool moveToClick = false;
    private Vector3 clickTarget;

    public void SetDead(bool dead)
    {
        isDead = dead;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDead)
        {
            animator.SetBool("Run", false);
            return;
        }

        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.W)) verticalInput += 1f;
        if (Input.GetKey(KeyCode.S)) verticalInput -= 1f;
        if (Input.GetKey(KeyCode.A)) horizontalInput -= 1f;
        if (Input.GetKey(KeyCode.D)) horizontalInput += 1f;

        bool isMovingKeyboard = (horizontalInput != 0 || verticalInput != 0);

        // Mouse sol tuşu ile hareket etme
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                clickTarget = hit.point;
                moveToClick = true;
            }
        }

        if (isMovingKeyboard)
        {
            moveToClick = false;
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0f;
            right.y = 0f;
            moveDirection = (forward * verticalInput + right * horizontalInput).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
        }
        else if (moveToClick)
        {
            Vector3 direction = (clickTarget - transform.position);
            direction.y = 0f;
            float distance = direction.magnitude;

            if (distance > 0.1f)
            {
                moveDirection = direction.normalized;

                if (moveDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 15f);
                }
            }
            else
            {
                moveToClick = false;
                moveDirection = Vector3.zero;
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }

        // Hareket varsa animasyon true, yoksa false
        // >>> Buradaki satır animasyonun her karede güncellenmesini sağlar <<<
        animator.SetBool("Run", moveDirection.magnitude > 0f);
    }

    void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.velocity = moveDirection * moveSpeed + new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}