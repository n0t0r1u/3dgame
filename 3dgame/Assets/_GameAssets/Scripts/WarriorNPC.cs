using UnityEngine;

public class WarriorNPC : MonoBehaviour
{
    public NPC npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var dialogueUI = FindObjectOfType<DialogueUI>();
            if (dialogueUI != null)
            {
                var pqs = other.GetComponent<PlayerQuestSystem>();
                dialogueUI.ShowDialogue(npc, pqs, npc.GetNextDialogue());
            }
        }
    }
}