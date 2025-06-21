using UnityEngine;
public enum QuestType { KillZombie, CollectItem, TalkToNPC }

[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public bool isCompleted = false;
    public QuestType questType;

    public int killTarget = 0;
    public int killCount = 0;

    public bool CheckCompletion()
    {
        if (questType == QuestType.KillZombie)
            return killCount >= killTarget;
        // Diğer görev türleri için ek kontrol
        return false;
    }

    public void ProgressKill()
    {
        if (questType == QuestType.KillZombie && !isCompleted)
        {
            killCount++;
            if (CheckCompletion()) isCompleted = true;
        }
    }
}