//using UnityEngine;

//public class AnimationController : MonoBehaviour
//{
//    public Animator animator;
//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        animator = GetComponent<Animator>();

//    }

//    // Update is called once per frame
//    void Update()
//    {

//        if (animator != null)
//        {
//            if (Input.GetKey(KeyCode.W))
//            {
//                animator.SetBool("Run", true);
//            }
//            if (Input.GetKeyUp(KeyCode.W))a
//            {
//                {
//                    // Hi�bir tu� bas�l� de�ilse "Run" animasyonunu durdur
//                    animator.SetBool("Run", false);
//                }
//            }
//            if (Input.GetKey(KeyCode.A))
//            {
//                animator.SetBool("Run", true);
//            }
//            if (Input.GetKeyUp(KeyCode.A))
//            {
//                {
//                    // Hi�bir tu� bas�l� de�ilse "Run" animasyonunu durdur
//                    animator.SetBool("Run", false);
//                }
//            }
//            if (Input.GetKey(KeyCode.S))
//            {
//                animator.SetBool("Run", true);
//            }
//            if (Input.GetKeyUp(KeyCode.S))
//            {
//                {
//                    // Hi�bir tu� bas�l� de�ilse "Run" animasyonunu durdur
//                    animator.SetBool("Run", false);
//                }
//            }
//            if (Input.GetKey(KeyCode.D))
//            {
//                animator.SetBool("Run", true);
//            }
//            if (Input.GetKeyUp(KeyCode.D))
//            {
//                {
//                    // Hi�bir tu� bas�l� de�ilse "Run" animasyonunu durdur
//                    animator.SetBool("Run", false);
//                }
//            }
//        }

//    }
//}
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private KeyCode[] movementKeys = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    private int comboIndex = 0;
    private float attackTimer = 0f;
    private float comboCooldown = 0.5f; // animasyonlar arası süre

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator == null) return;

        // Hareket kontrolü
        bool isMoving = false;
        foreach (KeyCode key in movementKeys)
        {
            if (Input.GetKey(key))
            {
                isMoving = true;
                break;
            }
        }
        animator.SetBool("Run", isMoving);

        // Saldırı kontrolü (basılı tutulunca)
        bool isAttacking = Input.GetMouseButton(0);
        animator.SetBool("IsAttacking", isAttacking);

        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= comboCooldown)
            {
                attackTimer = 0f;
                comboIndex = (comboIndex + 1) % 5; // 0–4arası döner
                animator.SetInteger("ComboIndex", comboIndex);
            }
        }
        else
        {
            comboIndex = 0;
            attackTimer = 0f;
            animator.SetInteger("ComboIndex", comboIndex);
        }
    }
}

