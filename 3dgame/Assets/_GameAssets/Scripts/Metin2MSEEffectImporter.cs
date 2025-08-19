using UnityEngine;
using System.Collections.Generic;

public class Metin2MSEEffectImporter : MonoBehaviour
{
    // dragon_tex ve special_tex texture'larını Resources/Effects/ klasörüne uygun şekilde .png/.jpg olarak atmalısın!
    public string dragonTextureName = "dragon_tex";
    public string specialTextureName = "special_tex";

    private struct ParticleGroupData
    {
        public string textureName;
        public int maxParticles;
        public float duration;
        public bool loop;
        public float rateOverTime;
        public float startLifetime;
        public float startSpeed;
        public Vector3 startSize;
        public ParticleSystemShapeType shapeType;
        public Vector3 shapeParams;
        public Color colorRGB;
        public GradientAlphaKey[] alphaKeys;
        public Vector3 positionOffset;
        public float rotationSpeed;
        public bool enableGravity;
        public float gravity;
        public float scaleStart;
        public float scaleEnd;
        public int billboardType;
    }

    void Start()
    {
        // Her Group Particle için ayarları doldur
        List<ParticleGroupData> groups = new List<ParticleGroupData>();

        // 1. Group Particle (dragon_tex)
        groups.Add(new ParticleGroupData
        {
            textureName = dragonTextureName,
            maxParticles = 30,
            duration = 0.5f,
            loop = true,
            rateOverTime = 20f,
            startLifetime = 1.3f,
            startSpeed = 0.2f,
            startSize = new Vector3(40, 65, 1),
            shapeType = ParticleSystemShapeType.Box,
            shapeParams = new Vector3(60, 70, 60),
            colorRGB = new Color(0.243f, 0.243f, 0.243f, 1f),
            alphaKeys = new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(0.4f, 0.4f),
                new GradientAlphaKey(0f, 1f)
            },
            positionOffset = new Vector3(0f, 2.828f, 0f),
            rotationSpeed = 0f,
            enableGravity = false,
            gravity = 0f,
            scaleStart = 1f,
            scaleEnd = 2f,
            billboardType = 1
        });

        // 2. Group Particle (dragon_tex)
        groups.Add(new ParticleGroupData
        {
            textureName = dragonTextureName,
            maxParticles = 10,
            duration = 0.5f,
            loop = true,
            rateOverTime = 20f,
            startLifetime = 1.3f,
            startSpeed = 0.2f,
            startSize = new Vector3(45, 45, 1),
            shapeType = ParticleSystemShapeType.Sphere,
            shapeParams = new Vector3(80, 0, 0), // radius: 80
            colorRGB = new Color(0.655f, 0.247f, 0.133f, 1f),
            alphaKeys = new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(1f, 0.45f),
                new GradientAlphaKey(0f, 1f)
            },
            positionOffset = new Vector3(0f, 0f, 0f),
            rotationSpeed = 20f,
            enableGravity = true,
            gravity = -20f,
            scaleStart = 0.2f,
            scaleEnd = 0.4f,
            billboardType = 1
        });

        // 3. Group Particle (dragon_tex)
        groups.Add(new ParticleGroupData
        {
            textureName = dragonTextureName,
            maxParticles = 30,
            duration = 0.5f,
            loop = true,
            rateOverTime = 20f,
            startLifetime = 1.3f,
            startSpeed = 0f,
            startSize = new Vector3(40, 40, 1),
            shapeType = ParticleSystemShapeType.Box,
            shapeParams = new Vector3(0, 80, 0),
            colorRGB = new Color(1f, 0.741f, 0.235f, 1f),
            alphaKeys = new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(0.66f, 0.42f),
                new GradientAlphaKey(0f, 1f)
            },
            positionOffset = new Vector3(0f, 1.803f, 0f),
            rotationSpeed = 20f,
            enableGravity = false,
            gravity = 0f,
            scaleStart = 1f,
            scaleEnd = 2f,
            billboardType = 1
        });

        // 4. Group Particle (special_tex)
        groups.Add(new ParticleGroupData
        {
            textureName = specialTextureName,
            maxParticles = 30,
            duration = 0.5f,
            loop = true,
            rateOverTime = 20f,
            startLifetime = 0.9f,
            startSpeed = 0f,
            startSize = new Vector3(70, 20, 1),
            shapeType = ParticleSystemShapeType.Sphere,
            shapeParams = new Vector3(50, 0, 0),
            colorRGB = new Color(0.870f, 0.117f, 0.117f, 1f), // RGB'yi ortalama alarak örnekledik
            alphaKeys = new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(0.86f, 0.264f),
                new GradientAlphaKey(0f, 1f)
            },
            positionOffset = new Vector3(0f, 5.172f, 0f),
            rotationSpeed = 0f,
            enableGravity = false,
            gravity = 0f,
            scaleStart = 1f,
            scaleEnd = 1f,
            billboardType = 2
        });

        // 5. Group Particle (special_tex)
        groups.Add(new ParticleGroupData
        {
            textureName = specialTextureName,
            maxParticles = 30,
            duration = 0.5f,
            loop = true,
            rateOverTime = 20f,
            startLifetime = 1.3f,
            startSpeed = 0f,
            startSize = new Vector3(30, 30, 1),
            shapeType = ParticleSystemShapeType.Box,
            shapeParams = new Vector3(60, 100, 60),
            colorRGB = new Color(0.898f, 0.196f, 0.196f, 1f),
            alphaKeys = new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(0.42f, 0.42f),
                new GradientAlphaKey(0f, 1f)
            },
            positionOffset = new Vector3(0f, 0.679f, 0f),
            rotationSpeed = 20f,
            enableGravity = false,
            gravity = 0f,
            scaleStart = 1f,
            scaleEnd = 2f,
            billboardType = 1
        });

        // 6. Group Particle (special_tex)
        groups.Add(new ParticleGroupData
        {
            textureName = specialTextureName,
            maxParticles = 20,
            duration = 0.5f,
            loop = true,
            rateOverTime = 20f,
            startLifetime = 1.5f,
            startSpeed = 30f,
            startSize = new Vector3(90, 90, 1),
            shapeType = ParticleSystemShapeType.Box,
            shapeParams = new Vector3(70, 100, 70),
            colorRGB = new Color(0.631f, 0.569f, 0.361f, 1f),
            alphaKeys = new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(1f, 0.225f),
                new GradientAlphaKey(0.64f, 0.451f),
                new GradientAlphaKey(0.38f, 0.621f),
                new GradientAlphaKey(0.281f, 0.846f),
                new GradientAlphaKey(0f, 1f)
            },
            positionOffset = new Vector3(0f, -4.245f, 0f),
            rotationSpeed = 0f,
            enableGravity = false,
            gravity = 0f,
            scaleStart = 0.1f,
            scaleEnd = 0.2f,
            billboardType = 1
        });

        // Her particle grubunu instantiate et
        foreach (var group in groups)
        {
            CreateParticleGroup(group);
        }
    }

    private void CreateParticleGroup(ParticleGroupData data)
    {
        GameObject go = new GameObject("Metin2Effect_" + data.textureName);
        go.transform.parent = this.transform;
        go.transform.localPosition = data.positionOffset;

        var ps = go.AddComponent<ParticleSystem>();
        var main = ps.main;
        var emission = ps.emission;
        var shape = ps.shape;
        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        var colorOverLifetime = ps.colorOverLifetime;

        // Main
        //main.duration = data.duration;
        main.loop = data.loop;
        main.maxParticles = data.maxParticles;
        main.startLifetime = data.startLifetime;
        main.startSpeed = data.startSpeed;
        main.startSize = data.startSize.x; // Unity tek eksenle başlıyor, scale için aşağıda ayar
        main.gravityModifier = data.enableGravity ? data.gravity : 0f;

        // Color
        main.startColor = data.colorRGB;

        // Emission
        emission.rateOverTime = data.rateOverTime;

        // Shape
        shape.enabled = true;
        shape.shapeType = data.shapeType;
        if (data.shapeType == ParticleSystemShapeType.Box)
            shape.scale = data.shapeParams;
        else if (data.shapeType == ParticleSystemShapeType.Sphere)
            shape.radius = data.shapeParams.x;

        // Color over lifetime (Alpha animasyonu)
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(data.colorRGB, 0.0f),
                new GradientColorKey(data.colorRGB, 1.0f)
            },
            data.alphaKeys
        );
        colorOverLifetime.color = grad;

        // Scale over lifetime
        var sizeOverLifetime = ps.sizeOverLifetime;
        sizeOverLifetime.enabled = true;
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, data.scaleStart);
        curve.AddKey(1.0f, data.scaleEnd);
        sizeOverLifetime.size = new ParticleSystem.MinMaxCurve(1.0f, curve);

        // Rotation (varsa)
        if (data.rotationSpeed > 0f)
        {
            var rotationOverLifetime = ps.rotationOverLifetime;
            rotationOverLifetime.enabled = true;
            rotationOverLifetime.z = data.rotationSpeed * Mathf.Deg2Rad;
        }

        // Renderer ayarları
        // BillboardType: 1 = Billboard, 2 = Stretched billboard (Unity'de uygun şekilde ayarla)
        renderer.renderMode = data.billboardType == 2
            ? ParticleSystemRenderMode.Stretch
            : ParticleSystemRenderMode.Billboard;

        // Texture ve materyal
        Texture2D tex = Resources.Load<Texture2D>("Effects/" + data.textureName);
        if (tex != null)
        {
            var mat = new Material(Shader.Find("Particles/Standard Unlit"));
            mat.mainTexture = tex;
            mat.renderQueue = 3000;
            mat.SetFloat("_Mode", 2); // Transparent
            renderer.material = mat;
        }
    }
}