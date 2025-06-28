using UnityEngine;
using System.Linq;

public class SkinnedMeshChanger : MonoBehaviour
{
    public SkinnedMeshRenderer defaultRenderer; // Karakter üzerindeki mevcut mesh
    public SkinnedMeshRenderer newClothesRenderer; // Yeni kıyafet prefabından alınan renderer

    public void ChangeSkinnedMesh()
    {
        defaultRenderer.sharedMesh = newClothesRenderer.sharedMesh;
        defaultRenderer.materials = newClothesRenderer.sharedMaterials;

        // Karakterin kemiklerini isimle eşle
        var characterBones = defaultRenderer.rootBone.GetComponentsInParent<Transform>(true)
            .Concat(defaultRenderer.rootBone.GetComponentsInChildren<Transform>(true)).Distinct().ToArray();
        var boneMap = characterBones.ToDictionary(b => b.name, b => b);

        // Yeni kıyafetin kemiklerini karakterin kemikleriyle eşleştir
        defaultRenderer.bones = newClothesRenderer.bones
            .Select(b => boneMap.TryGetValue(b.name, out var cb) ? cb : null)
            .ToArray();

        // rootBone'u da eşleştir
        if (newClothesRenderer.rootBone != null && boneMap.TryGetValue(newClothesRenderer.rootBone.name, out var mappedRootBone))
            defaultRenderer.rootBone = mappedRootBone;
    }

    public void Start()
    {
        ChangeSkinnedMesh();
    }
}