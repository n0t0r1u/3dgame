using UnityEngine;

public class PortalBlueInteraction : MonoBehaviour
{
    public DialogueUI dialogueUI;         // Inspector’dan bağla
    public Transform teleportDestination;  // Inspector’dan bağla (ışınlanacak hedef pozisyon)
    private bool playerInTrigger = false;
    private GameObject playerObj;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerObj = other.gameObject;
            playerInTrigger = true;
            // Portal mesajı açılır
            dialogueUI.ShowCustomDialogue(
                "Liderin yanına ışınlanmak istiyor musun? Emin misin?", 
                OnAccepted, 
                OnDeclined
            );
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            dialogueUI.HideDialogue();
        }
    }

    void OnAccepted()
    {
        if (playerObj != null && teleportDestination != null)
        {
            playerObj.transform.position = teleportDestination.position;
            // Rigidbody sıfırlama (opsiyonel)
            var rb = playerObj.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            // Hareket scriptini 1 sn devre dışı bırakmak için:
            var movement = playerObj.GetComponent<PlayerMovement1>();
            if (movement != null) {
                movement.enabled = false;
                StartCoroutine(ReEnableMovementAfterDelay(movement, 1f));
            }
        }
        dialogueUI.HideDialogue();
    }

    void OnDeclined()
    {
        dialogueUI.HideDialogue();
    }

    System.Collections.IEnumerator ReEnableMovementAfterDelay(MonoBehaviour script, float delay)
    {
        yield return new WaitForSeconds(delay);
        script.enabled = true;
    }
}