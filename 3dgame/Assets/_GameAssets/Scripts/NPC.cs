using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName;
    public string[] dialogues;
    public Quest questToGive;

    private int dialogueIndex = 0;

    public string GetNextDialogue()
    {
        if (dialogueIndex < dialogues.Length)
            return dialogues[dialogueIndex++];
        else
            return null;
    }

    public void ResetDialogue()
    {
        dialogueIndex = 0;
    }
}