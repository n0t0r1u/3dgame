using UnityEngine;

public class ArmorSwapper : MonoBehaviour
{
    public GameObject player; // Sahnede bulunan ana karakter (bone rig burada)
    public GameObject armorPrefab; // Prefab ile gelen yeni zırh (SkinnedMeshRenderer’lı)

    public void SwapArmor()
    {
        // Prefabdan zırhı oluştur (ama sahneye ekleme, sadece mesh ve kemikler alınacak)
        GameObject armorInstance = Instantiate(armorPrefab);

        // Prefabdan SkinnedMeshRenderer’ı al
        SkinnedMeshRenderer armorSMR = armorInstance.GetComponentInChildren<SkinnedMeshRenderer>();

        // Sahnede ana karakterin kemiklerini al
        SkinnedMeshRenderer playerSMR = player.GetComponentInChildren<SkinnedMeshRenderer>();
        Transform[] playerBones = playerSMR.bones;
        Transform playerRootBone = playerSMR.rootBone;

        // Yeni bir GameObject oluştur ve SkinnedMeshRenderer ekle
        GameObject newArmorGO = new GameObject("EquippedArmor");
        SkinnedMeshRenderer newArmorSMR = newArmorGO.AddComponent<SkinnedMeshRenderer>();

        // Mesh ve materyali ata
        newArmorSMR.sharedMesh = armorSMR.sharedMesh;
        newArmorSMR.materials = armorSMR.materials;

        // Kemikleri ve rootBone’u ata
        newArmorSMR.bones = playerBones;
        newArmorSMR.rootBone = playerRootBone;

        // Yeni zırhı ana karakterin altına ekle
        newArmorGO.transform.SetParent(player.transform);

        // Eski armorları yok et (ekranı temizlemek için)
        // ...

        // Temporary armor prefabını yok et
        Destroy(armorInstance);
    }
    public void Start()
    {
        SwapArmor();
    }
}