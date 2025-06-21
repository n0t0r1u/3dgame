using UnityEngine;

[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public bool isCompleted = false;

    // Zombi öldürme görevi için:
    public int zombieKillTarget = 10;
    public int zombieKillCount = 0;

    public bool CheckCompletion()
    {
        return zombieKillCount >= zombieKillTarget;
    }
}