using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
    public Transform player;          // Işınlanacak oyuncu
    public Transform destination;     // Hedef nokta

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && player != null && destination != null)
        {
            player.position = destination.position;

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Hareket scriptini bul ve 1 saniyeliğine devre dışı bırak
            MonoBehaviour movementScript = player.GetComponent<MonoBehaviour>(); // Bunu PlayerMovement1 ile değiştir
            if (movementScript != null)
            {
                movementScript.enabled = false;
                StartCoroutine(EnableMovementAfterDelay(movementScript, 1f));
            }
        }
    }

    IEnumerator EnableMovementAfterDelay(MonoBehaviour movementScript, float delay)
    {
        yield return new WaitForSeconds(delay);
        movementScript.enabled = true;
    }
}