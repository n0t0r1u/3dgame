using UnityEngine;
using TMPro;
using System.Text;

public class QuestUI : MonoBehaviour
{
    public PlayerQuestSystem playerQuestSystem;
    public TMP_Text questListText;
    public GameObject questPanel;

    void Start()
    {
        if (questPanel != null)
            questPanel.SetActive(false);
    }

    void Update()
    {
        UpdateQuestList();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q basıldı. Panel durumu: " + questPanel.activeSelf);
            if (questPanel != null)
            {
                bool yeniDurum = !questPanel.activeSelf;
                questPanel.SetActive(yeniDurum);
                if (yeniDurum)
                    UpdateQuestList();
            }
        }
    }

    public void UpdateQuestList()
    {
        if (playerQuestSystem == null || questListText == null) return;

        StringBuilder sb = new StringBuilder();
        foreach (var quest in playerQuestSystem.activeQuests)
        {
            if (quest.isCompleted) continue;
            sb.AppendLine($"{quest.questName}");
            sb.AppendLine(quest.description);
            if (quest.questType == QuestType.KillZombie)
            sb.AppendLine($"Zombiler öldürüldü: {quest.killCount} / {quest.killTarget}");
            sb.AppendLine();
            if (quest.questType == QuestType.KillBoss)
            sb.AppendLine($"Boss öldürüldü: {quest.killCount} / {quest.killTarget}");
            sb.AppendLine();
            
        }
        questListText.text = sb.ToString();
    }
}