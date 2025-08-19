using UnityEngine;

public class MSEParticleLoader : MonoBehaviour
{
    void Start()
    {
        GameObject particleObj = new GameObject("MSE_Particle");
        ParticleSystem ps = particleObj.AddComponent<ParticleSystem>();
        var main = ps.main;
        var shape = ps.shape;
        var emission = ps.emission;

        // Örnek veriler .mse'den alınarak atanmış
        main.startLifetime = 1f;
        main.startSpeed = 0f;
        main.startSize = 1f;
        main.loop = false;
        main.duration = 1f;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 100f;
        emission.rateOverTime = 5f;

        ps.Play();
    }
}
