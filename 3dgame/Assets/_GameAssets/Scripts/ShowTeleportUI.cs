using UnityEngine;

public class ShowTeleportUI : MonoBehaviour
{
    public GameObject teleportPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            teleportPanel.SetActive(!teleportPanel.activeSelf);
        }
    }
}