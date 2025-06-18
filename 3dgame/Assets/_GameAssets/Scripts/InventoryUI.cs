using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform slotsParent;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I tuşuna basıldı");
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            if (inventoryPanel.activeSelf)
                UpdateUI();
        }
    }

    public void UpdateUI()
    {
        // Önce eski slotları temizle
        foreach (Transform child in slotsParent)
            Destroy(child.gameObject);

        // Envanterdeki her item için slot oluştur
        foreach (var slot in Inventory.Instance.items)
        {
            GameObject slotGO = Instantiate(slotPrefab, slotsParent);
            // Slot’a item ikonunu ve ismini/adetini yerleştir
            Image iconImage = slotGO.transform.Find("Icon").GetComponent<Image>();
            Text labelText = slotGO.transform.Find("Label").GetComponent<Text>();

            if (iconImage != null && slot.item.icon != null)
                iconImage.sprite = slot.item.icon;
            if (labelText != null)
                labelText.text = slot.item.itemName + " x" + slot.count;
        }
    }
}