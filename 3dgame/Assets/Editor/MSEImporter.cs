using UnityEngine;
using UnityEditor;
using System.IO;

public class MSEImporter : EditorWindow
{
    [MenuItem("Tools/MSE to ParticleSystem")]
    public static void ImportMSE()
    {
        string path = EditorUtility.OpenFilePanel("Select .mse file", "", "mse");
        if (string.IsNullOrEmpty(path)) return;

        string[] lines = File.ReadAllLines(path);
        GameObject go = new GameObject("ImportedEffect");
        var ps = go.AddComponent<ParticleSystem>();
        var main = ps.main;

        foreach (string line in lines)
        {
            string[] parts = line.Trim().Split(' ');
            if (parts.Length == 0) continue;

            switch (parts[0])
            {
                case "LifeTime":
                    main.duration = float.Parse(parts[1]);
                    break;
                case "ParticleLifeTime":
                    main.startLifetime = float.Parse(parts[1]);
                    break;
                case "Size":
                    main.startSize = float.Parse(parts[1]);
                    break;
                case "Color":
                    float r = float.Parse(parts[1]);
                    float g = float.Parse(parts[2]);
                    float b = float.Parse(parts[3]);
                    main.startColor = new Color(r, g, b);
                    break;
                case "Texture":
                    string texPath = parts[1].Replace("\"", "").Replace("d:/ymir work/", "Assets/Ymir/");
                    var renderer = ps.GetComponent<ParticleSystemRenderer>();
                    var tex = AssetDatabase.LoadAssetAtPath<Texture2D>(texPath);
                    if (tex != null)
                    {
                        var mat = new Material(Shader.Find("Particles/Standard Unlit"));
                        mat.mainTexture = tex;
                        renderer.material = mat;
                    }
                    else
                    {
                        Debug.LogWarning("Texture not found: " + texPath);
                    }
                    break;
            }
        }

        string savePath = $"Assets/Effects/Prefabs/{go.name}.prefab";
        Directory.CreateDirectory("Assets/Effects/Prefabs");
        PrefabUtility.SaveAsPrefabAsset(go, savePath);
        GameObject.DestroyImmediate(go);
        EditorUtility.DisplayDialog("Done", "MSE converted to prefab:\n" + savePath, "OK");
    }
}
