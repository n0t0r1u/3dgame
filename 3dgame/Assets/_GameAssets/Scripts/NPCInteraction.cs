using UnityEngine;
using UnityEngine.UI; // Eğer Text veya Button kullanıyorsan

public class NPCInteraction : MonoBehaviour
{
    public PlayerQuestSystem playerQuestSystem;
    public GameObject dialogueUI;
    public Text dialogueText; // UI'daki yazı alanı (Inspector'dan bağla)

    private NPC currentNPC;
/*************  ✨ Windsurf Command ⭐  *************/
        /// <summary>
        /// Etkileşim mümkünse ve E tuşuna basıldığında NPC ile diyaloğa girer.
        /// </summary>
/*******  1eda7972-2d91-470a-ae1f-0b5f2f305ea9  *******/    private bool canInteract = false;

    void Update()
    {
        // Sadece yakınsan ve E'ye bastıysan
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            dialogueUI.SetActive(true);
            ShowNextDialogue();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.GetComponent<NPC>();
            canInteract = true;
            // Burada UI açılmaz, E tuşunu bekliyoruz
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            canInteract = false;
            dialogueUI.SetActive(false);
            currentNPC = null;
        }
    }

    public void ShowNextDialogue()
    {
        if (currentNPC == null) return;

        string dialogue = currentNPC.GetNextDialogue();
        if (dialogue != null)
        {
            dialogueText.text = dialogue; // Diyaloğu ekrana yaz
        }
        else
        {
            // Görev verilecekse burada ver
            if (currentNPC.questToGive != null && !currentNPC.questToGive.isCompleted)
            {
                playerQuestSystem.AcceptQuest(currentNPC.questToGive);
            }
            currentNPC.ResetDialogue();
            dialogueUI.SetActive(false);
        }
    }
}