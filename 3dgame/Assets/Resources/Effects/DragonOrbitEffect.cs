using UnityEngine;

public class DragonOrbitEffect : MonoBehaviour
{
    public float orbitRadius = 2f;
    public float orbitSpeed = 40f; // derece/sn
    public float quadScale = 2f;   // Dragon görselinin büyüklüğü
    public string dragonTexturePath = "Effects/dragon_tex"; // Resources/Effects/dragon_tex.png

    private GameObject dragonQuad;
    private float currentAngle = 0f;

    void Start()
    {
        // Quad oluştur
        dragonQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        dragonQuad.transform.parent = this.transform;
        dragonQuad.transform.localScale = Vector3.one * quadScale;
        dragonQuad.transform.localPosition = GetOrbitPosition(0);
        dragonQuad.transform.localRotation = Quaternion.identity;

        // Material ata
        Texture2D dragonTexture = Resources.Load<Texture2D>(dragonTexturePath);
        if (dragonTexture != null)
        {
            var mat = new Material(Shader.Find("Unlit/Transparent"));
            mat.mainTexture = dragonTexture;
            dragonQuad.GetComponent<MeshRenderer>().material = mat;
        }
        // Kendi collider'ını sil (gerek yok)
        Destroy(dragonQuad.GetComponent<Collider>());
    }

    void Update()
    {
        // Dairesel hareket
        currentAngle += orbitSpeed * Time.deltaTime;
        currentAngle %= 360f;
        dragonQuad.transform.localPosition = GetOrbitPosition(currentAngle);

        // Her zaman karaktere doğru baksın (kamera için istersen Camera.main.transform.position da olur)
        dragonQuad.transform.LookAt(transform.position + Vector3.up * 1.5f);
    }

    Vector3 GetOrbitPosition(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad) * orbitRadius, 1.5f, Mathf.Sin(rad) * orbitRadius);
    }
}