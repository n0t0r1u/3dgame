using UnityEngine;

public class Metin2DragonAndFireEffect : MonoBehaviour
{
    [Header("Dragon")]
    public string dragonPrefabPath = "Effects/dragon_tex"; // Resources/Effects/dragon_tex.fbx
    public float dragonOrbitRadius = 2.0f;
    public float dragonOrbitSpeed = 40.0f; // derece/sn

    [Header("Fire")]
    public string fireTexturePath = "Effects/special_tex"; // Resources/Effects/special_tex.png
    public int fireParticleCount = 60;
    public float fireRadius = 2.0f;
    public float fireHeight = 2.0f;

    private GameObject dragonInstance;
    private float dragonAngle = 0f;

    void Start()
    {
        // --- DRAGON ---
        var dragonPrefab = Resources.Load<GameObject>(dragonPrefabPath);
        if (dragonPrefab != null)
        {
            dragonInstance = Instantiate(dragonPrefab, transform);
            dragonInstance.transform.localPosition = GetDragonOrbitPosition(0);
            dragonInstance.transform.localRotation = Quaternion.identity;
        }

        // --- FIRE PARTICLES ---
        GameObject fireGO = new GameObject("SpecialFireEffect");
        fireGO.transform.parent = this.transform;
        fireGO.transform.localPosition = Vector3.zero;

        var ps = fireGO.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.loop = true;
        main.startLifetime = 0.8f;
        main.startSpeed = 1.5f;
        main.startSize = 1f;
        main.maxParticles = fireParticleCount;

        var emission = ps.emission;
        emission.rateOverTime = fireParticleCount * 2;
        emission.burstCount = 1;

        var shape = ps.shape;
        shape.enabled = true;
        shape.shapeType = ParticleSystemShapeType.Donut;
        shape.radius = fireRadius;
        shape.donutRadius = 0.1f;
        shape.arcMode = ParticleSystemShapeMultiModeValue.Loop;
        shape.arcSpread = 360f;

        var colorOverLifetime = ps.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.yellow, 0f),
                new GradientColorKey(Color.red, 0.5f),
                new GradientColorKey(Color.black, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0.0f, 0.0f),
                new GradientAlphaKey(1.0f, 0.1f),
                new GradientAlphaKey(0.0f, 1.0f)
            }
        );
        colorOverLifetime.color = grad;

        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        Texture2D fireTex = Resources.Load<Texture2D>(fireTexturePath);
        if (fireTex != null)
        {
            var mat = new Material(Shader.Find("Particles/Standard Unlit"));
            mat.mainTexture = fireTex;
            mat.SetFloat("_Mode", 2);
            mat.renderQueue = 3000;
            renderer.material = mat;
        }
        renderer.renderMode = ParticleSystemRenderMode.Billboard;

        // Fire particles yukarıya doğru yükselsin:
        var velocity = ps.velocityOverLifetime;
        velocity.enabled = true;
        velocity.y = fireHeight;
    }

    void Update()
    {
        // Dragon modeli karakterin etrafında dolaşsın (yörüngede döndür)
        if (dragonInstance != null)
        {
            dragonAngle += dragonOrbitSpeed * Time.deltaTime;
            dragonAngle %= 360f;
            dragonInstance.transform.localPosition = GetDragonOrbitPosition(dragonAngle);
            dragonInstance.transform.LookAt(transform.position + Vector3.up * 1.5f); // Yüze bak
        }
    }

    Vector3 GetDragonOrbitPosition(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * dragonOrbitRadius, 1.5f, Mathf.Sin(rad) * dragonOrbitRadius);
    }
}