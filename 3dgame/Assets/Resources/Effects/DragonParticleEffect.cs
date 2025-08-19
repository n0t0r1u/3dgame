using UnityEngine;

public class DragonParticleEffect : MonoBehaviour
{
    public string texturePath = "Effects/dragon_tex"; // Resources/Effects/dragon_tex.png
    public float orbitRadius = 2.0f;
    public float orbitHeight = 1.5f;
    public float orbitSpeed = 40f;

    private ParticleSystem ps;
    private float angle = 0f;

    void Start()
    {
        // Particle System oluştur
        GameObject go = new GameObject("DragonParticle");
        go.transform.parent = this.transform;
        go.transform.localPosition = Vector3.zero;

        ps = go.AddComponent<ParticleSystem>();
        var main = ps.main;
        main.loop = true;
        main.startLifetime = 999f;
        main.startSize = 2f;
        main.maxParticles = 1;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        var emission = ps.emission;
        emission.rateOverTime = 0;
        emission.rateOverDistance = 0;

        var shape = ps.shape;
        shape.enabled = false;

        var renderer = ps.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Billboard;

        // Material ve texture ata
        Texture2D dragonTex = Resources.Load<Texture2D>(texturePath);
        if (dragonTex != null)
        {
            var mat = new Material(Shader.Find("Particles/Standard Unlit"));
            mat.mainTexture = dragonTex;
            mat.SetFloat("_Mode", 2);
            mat.renderQueue = 3000;
            renderer.material = mat;
        }

        // İlk partikülü spawnla
        var particles = new ParticleSystem.Particle[1];
        particles[0].position = GetOrbitPosition(0);
        particles[0].startSize = main.startSize.constant;
        particles[0].startLifetime = main.startLifetime.constant;
        particles[0].remainingLifetime = main.startLifetime.constant;
        particles[0].rotation3D = Vector3.zero;
        particles[0].startColor = Color.white;
        ps.SetParticles(particles, 1);
    }

    void Update()
    {
        // Partikülün pozisyonunu güncelle (dönsün)
        angle += orbitSpeed * Time.deltaTime;
        angle %= 360f;
        var particles = new ParticleSystem.Particle[1];
        int count = ps.GetParticles(particles);
        if (count > 0)
        {
            particles[0].position = GetOrbitPosition(angle);
            ps.SetParticles(particles, 1);
        }
    }

    Vector3 GetOrbitPosition(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * orbitRadius, orbitHeight, Mathf.Sin(rad) * orbitRadius);
    }
}