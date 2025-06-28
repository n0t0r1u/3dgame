using UnityEngine;

public class ClothesChanger : MonoBehaviour
{
    public GameObject characterRoot; // Karakterin root GameObject'i (kemiklerin altında olduğu ana objeniz)
    public Transform armatureRoot; // Karakterin Armature (kemik sistemi) kökü
    public SkinnedMeshRenderer bodyMesh; // Karakterin ana vücut mesh renderer'ı

    // Kıyafet prefabı (SkinnedMeshRenderer içeren)
    public GameObject clothesPrefab;

    private GameObject currentClothesInstance;

    public void ChangeClothes()
    {
        if (currentClothesInstance != null)
            Destroy(currentClothesInstance);

        // Prefabdan yeni kıyafet instantiate et
        GameObject newClothes = Instantiate(clothesPrefab, bodyMesh.transform.parent);

        SkinnedMeshRenderer clothesRenderer = newClothes.GetComponent<SkinnedMeshRenderer>();

        // Karakterdeki kemiklerin Transforms'larını bul
        Transform[] characterBones = armatureRoot.GetComponentsInChildren<Transform>(true);
        Transform[] newBones = new Transform[clothesRenderer.bones.Length];

        for (int i = 0; i < newBones.Length; i++)
        {
            string boneName = clothesRenderer.bones[i].name;
            foreach (var t in characterBones)
            {
                if (t.name == boneName)
                {
                    newBones[i] = t;
                    break;
                }
            }

            if (newBones[i] == null)
            {
                Debug.LogError("Kemik bulunamadı: " + boneName);
            }
        }

        clothesRenderer.bones = newBones;
        clothesRenderer.rootBone = armatureRoot; // veya bodyMesh.rootBone

        currentClothesInstance = newClothes;
    }
}