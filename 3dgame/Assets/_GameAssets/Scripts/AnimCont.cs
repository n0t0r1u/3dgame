using UnityEditor.Animations;
using UnityEngine;

public class AnimCont : MonoBehaviour

    
{
    public Animator animator;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
     if (animator == null) return;
        // Hareket kontrolü
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        animator.SetBool("Run", isMoving);

        // Saldırı kontrolü
        bool isAttacking = Input.GetKey(KeyCode.Space);
        animator.SetBool("Attacking", isAttacking);

        if (isAttacking)
        {
            animator.SetBool("Run", false); // Saldırı sırasında koşma kapalı
        }
    }
}
