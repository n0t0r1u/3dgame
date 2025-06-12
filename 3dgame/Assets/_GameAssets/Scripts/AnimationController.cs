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
//            if (Input.GetKeyUp(KeyCode.W))
//            {
//                {
//                    // Hiçbir tuþ basýlý deðilse "Run" animasyonunu durdur
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
//                    // Hiçbir tuþ basýlý deðilse "Run" animasyonunu durdur
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
//                    // Hiçbir tuþ basýlý deðilse "Run" animasyonunu durdur
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
//                    // Hiçbir tuþ basýlý deðilse "Run" animasyonunu durdur
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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator != null)
        {
            bool isMoving = false;

            // Tüm hareket tuþlarýný kontrol et
            foreach (KeyCode key in movementKeys)
            {
                if (Input.GetKey(key))
                {
                    isMoving = true;
                    break;  // Eðer bir tuþa basýldýysa, hemen durdur
                }
            }

            // Animator parametresini güncelle
            animator.SetBool("Run", isMoving);
        }
    }
}

