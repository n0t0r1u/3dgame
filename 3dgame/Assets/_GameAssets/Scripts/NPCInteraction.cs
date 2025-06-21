using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public DialogueUI dialogueUI;
    public PlayerQuestSystem playerQuestSystem;

    private NPC currentNPC;
    private bool canInteract = false;

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            if (currentNPC != null)
            {
                string dialogueText = currentNPC.GetNextDialogue();
                dialogueUI.ShowDialogue(currentNPC, playerQuestSystem, dialogueText);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPC>();
            canInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canInteract = false;
            dialogueUI.HideDialogue();
            currentNPC = null;
        }
    }
}