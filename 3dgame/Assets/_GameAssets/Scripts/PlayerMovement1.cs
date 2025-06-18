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

    // Ekstra: AnimationController referansı
    private AnimationController animationController;

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
        animationController = GetComponent<AnimationController>();
    }

    void Update()
    {
        if (isDead)
        {
            animator.SetBool("Run", false);
            return;
        }

        // Saldırı sırasında hareket engeli:
        if (animationController != null && animationController.isAttacking)
        {
            moveDirection = Vector3.zero;
            moveToClick = false;
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