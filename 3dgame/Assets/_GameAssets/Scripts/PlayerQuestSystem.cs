using UnityEngine;
using System.Collections.Generic;

public class PlayerQuestSystem : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();

    public void AcceptQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
            activeQuests.Add(quest);
    }

    public void OnZombieKilled()
    {
        foreach (Quest quest in activeQuests)
        {
            if (!quest.isCompleted && quest.questType == QuestType.KillZombie)
                quest.ProgressKill();
        }
    }
}