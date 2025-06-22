using UnityEngine;
public enum QuestType { KillZombie, CollectItem, TalkToNPC }

[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public bool isCompleted = false;
    public QuestType questType;
    public string targetNpcName;

    public int killTarget = 0;
    public int killCount = 0;

    public bool CheckCompletion(GameObject player = null)
    {
        if (questType == QuestType.KillZombie)
            return killCount >= killTarget;
        // Diğer görev türleri için ek kontrol
        if (questType == QuestType.TalkToNPC && player != null)
        {
            // Hedef NPC'ye ulaşıldıysa tamamla
            PlayerQuestSystem pqs = player.GetComponent<PlayerQuestSystem>();
            return pqs != null && pqs.lastTalkedNpcName == targetNpcName;
        }
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