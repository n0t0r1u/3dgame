using UnityEngine;

public class WolfmanNPC : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerQuestSystem pqs = other.GetComponent<PlayerQuestSystem>();
            if (pqs != null)
            {
                pqs.OnTalkedToNPC("Wolfman");
            }
        }
    }
}