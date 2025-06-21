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
        Debug.Log("TestDialogueUI Start çalışıyor!");
        if (acceptButton == null)
            acceptButton = GameObject.Find("AcceptButton").GetComponent<Button>();
        if (declineButton == null)
            declineButton = GameObject.Find("DeclineButton").GetComponent<Button>();

        Debug.Log("acceptButton: " + acceptButton);
        Debug.Log("declineButton: " + declineButton);

        acceptButton.onClick.AddListener(() => Debug.Log("Accept tıklandı!"));
        declineButton.onClick.AddListener(() => Debug.Log("Decline tıklandı!"));
        Debug.Log("DialogueUI Start Çalıştı");
        dialoguePanel.SetActive(false);
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
    Debug.Log("OnAcceptQuest Çalıştı");

    if (currentNPC == null) { Debug.Log("currentNPC null!"); return; }
    if (playerQuestSystem == null) { Debug.Log("playerQuestSystem null!"); return; }
    if (currentNPC.questToGive == null) { Debug.Log("currentNPC.questToGive null!"); return; }

    playerQuestSystem.AcceptQuest(currentNPC.questToGive);
    Debug.Log("Görev kabul edildi: " + currentNPC.questToGive.questName);

    dialogueTextTMP.text = "Görev kabul edildi!";
    acceptButton.gameObject.SetActive(false);
    declineButton.gameObject.SetActive(false);
}

    void OnDeclineQuest()
    {
        dialogueTextTMP.text = "Görev reddedildi.";
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
        HideDialogue();
        // İstersen NPC’ye flag koyabilirsin: currentNPC.questDeclined = true;
    }

    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
        acceptButton.gameObject.SetActive(false);
        declineButton.gameObject.SetActive(false);
    }
}