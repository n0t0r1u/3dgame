using UnityEngine;

public class PlayerModelSwitcher : MonoBehaviour
{
    public GameObject modelA; // İlk model (ör: PlayerModelA)
    public GameObject modelB; // İkinci model (ör: PlayerModelB)
    public Animator animator; // Animator bileşeni
    public GameObject hair;
    private bool useModelA = true;
    public AnimCont animCont; // Animasyon kontrol bileşeni

    void Start()
    {
        SetModel(true); // Oyuna başlarken modelA açık olsun
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            useModelA = !useModelA;
            SetModel(useModelA);
        }
    }

    void SetModel(bool showA)
    {
        GameObject activeModel = showA ? modelA : modelB;
        Transform kafaObj = FindDeepChild(activeModel.transform, "Bip01 Head"); // az önceki fonksiyonu kullanabilirsin
        if (kafaObj != null)
        {
            // kafaObj artık aktif modeldeki "kafa" tag'li objedir
            // Burada istediğin işlemi yapabilirsin, ör:
            Debug.Log("Kafa objesi bulundu: " + kafaObj.name);
            hair.transform.SetParent(kafaObj, true);
        }
        else
        {
            Debug.LogWarning("Kafa objesi bulunamadı!");
        }
        modelB.transform.position = modelA.transform.position; // İki modelin pozisyonunu eşitle
        //hair.transform.SetParent(GameObject.FindWithTag("Kafa").transform); // Saç modelini aktif modele ata
        //hair.transform.SetParent(showA ? modelA.transform : modelB.transform); // Saç modelini aktif modele ata // Animator bileşenini al
        animCont.animator = activeModel.GetComponentInChildren<Animator>(); // Aktif modelin Animator bileşenini ata
        //animCont.animatorController = showA ? modelA.GetComponent<Animator>().runtimeAnimatorController : modelB.GetComponent<Animator>().runtimeAnimatorController;
        modelA.SetActive(showA);
        modelB.SetActive(!showA);
    }
    Transform FindDeepChild(Transform parent, string childName)
{
    foreach (Transform child in parent)
    {
        if (child.name == childName)
            return child;

        Transform result = FindDeepChild(child, childName);
        if (result != null)
            return result;
    }
    return null;
}

}