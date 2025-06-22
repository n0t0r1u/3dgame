using UnityEngine;
using System.Collections.Generic;

public class PlayerQuestSystem : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();
    public string lastTalkedNpcName = "";

    public void AcceptQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
            activeQuests.Add(quest);
    }

    public void OnBossKilled()
    {
         foreach (Quest quest in activeQuests)
    {
        // Yalnızca boss görevini ilerlet
        if (!quest.isCompleted &&
            quest.questType == QuestType.KillBoss &&
            quest.killTarget == 1 &&
            quest.questName == "BOSS'U YEN!")
        {
            quest.killCount++;
            if (quest.killCount >= quest.killTarget)
                quest.isCompleted = true;
        }
    }
    RemoveCompletedQuests();
    }

    public void OnZombieKilled()
    {
        foreach (Quest quest in activeQuests)
        {
            if (!quest.isCompleted && quest.questType == QuestType.KillZombie)
                quest.ProgressKill();
        }
    }
    public void RemoveCompletedQuests()
    {
        activeQuests.RemoveAll(q => q.isCompleted);
    }
    public void OnTalkedToNPC(string npcName)
    {
        lastTalkedNpcName = npcName;
        
        foreach (Quest quest in activeQuests)
        {
            
            if (!quest.isCompleted && quest.questType == QuestType.TalkToNPC && quest.targetNpcName == npcName)
                quest.isCompleted = true;
        }
    }
}