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

    void UpdateQuestList()
    {
        if (playerQuestSystem == null || questListText == null) return;

        StringBuilder sb = new StringBuilder();
        foreach (var quest in playerQuestSystem.activeQuests)
        {
            sb.AppendLine($"{quest.questName}");
            sb.AppendLine(quest.description);
            sb.AppendLine();
        }
        questListText.text = sb.ToString();
    }
}