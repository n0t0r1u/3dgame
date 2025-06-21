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

    public void ZombieKilled()
    {
        foreach (Quest quest in activeQuests)
        {
            if (!quest.isCompleted)
            {
                // Sadece Zombi öldürme görevini kontrol et, ismini veya tipini kontrol edebilirsin
                if (quest.questName == "Zombileri Yok Et" || quest.description.Contains("Enemy"))
                {
                    quest.zombieKillCount++;
                    Debug.Log("Zombi öldürüldü. Şu ana kadar: " + quest.zombieKillCount);

                    if (quest.CheckCompletion())
                    {
                        quest.isCompleted = true;
                        Debug.Log("Görev tamamlandı: " + quest.questName);
                        // İstersen UI veya ödül sistemi tetikleyebilirsin
                    }
                }
            }
        }
    }
}