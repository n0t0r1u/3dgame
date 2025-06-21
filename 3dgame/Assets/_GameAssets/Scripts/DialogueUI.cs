using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueTextTMP;
    public Button acceptButton;   // Inspector’dan bağla
    public Button declineButton;  // Inspector’dan bağla

    // NPC referansı ve quest system
    private NPC currentNPC;
    private PlayerQuestSystem playerQuestSystem;

    void Start()
    {
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);

        acceptButton.onClick.AddListener(OnAcceptQuest);
        declineButton.onClick.AddListener(OnDeclineQuest);
    }

    public void ShowDialogue(NPC npc, PlayerQuestSystem questSystem, string text)
    {
        dialoguePanel.SetActive(true);
        currentNPC = npc;
        playerQuestSystem = questSystem;
        dialogueTextTMP.text = text;

        // Eğer quest verilecekse butonları aç
        if (npc.questToGive != null && !npc.questToGive.isCompleted)
        {
            acceptButton.gameObject.SetActive(true);
            declineButton.gameObject.SetActive(true);
        }
        else
        {
            acceptButton.gameObject.SetActive(false);
            declineButton.gameObject.SetActive(false);
        }
    }

    void OnAcceptQuest()
    {
        if (currentNPC != null && playerQuestSystem != null && currentNPC.questToGive != null)
        {
            playerQuestSystem.AcceptQuest(currentNPC.questToGive);
            dialogueTextTMP.text = "Görev kabul edildi!";
            acceptButton.gameObject.SetActive(false);
            declineButton.gameObject.SetActive(false);
        }
    }

    void OnDeclineQuest()
    {
        dialogueTextTMP.text = "Görev reddedildi.";
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
        // İstersen NPC’ye flag koyabilirsin: currentNPC.questDeclined = true;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
    }
}