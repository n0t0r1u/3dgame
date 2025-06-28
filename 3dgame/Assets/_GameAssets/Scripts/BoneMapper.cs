using System.Linq;
using UnityEngine;

public class BoneMapper : MonoBehaviour
{
    public SkinnedMeshRenderer smr;
    public Transform[] characterBones;
    public Transform characterRootBone;

    public void MapBones()
    {
        var boneMap = characterBones.ToDictionary(b => b.name, b => b);
        smr.bones = smr.bones.Select(b => boneMap.TryGetValue(b.name, out var cb) ? cb : null).ToArray();
        smr.rootBone = boneMap.TryGetValue(smr.rootBone.name, out var rb) ? rb : null;
    }
}