using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform player;
    public Transform region1, region2, region3;

    public void TeleportToRegion1()
    {
        player.position = region1.position;
    }

    public void TeleportToRegion2()
    {
        player.position = region2.position;
    }

    public void TeleportToRegion3()
    {
        player.position = region3.position;
    }
}