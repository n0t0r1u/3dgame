using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public Transform slotsParent;
    public GameObject equippedPanel;
    public GameObject equippedSlotPrefab;
    public Transform equippedSlotsParent;

    void Start()
    {
        // Başlangıçta panelleri kapalı yapmak isterseniz buraya ekleyebilirsiniz.
        // inventoryPanel.SetActive(false);
        // equippedPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("I tuşuna basıldı");
            bool isActive = !inventoryPanel.activeSelf;
            inventoryPanel.SetActive(isActive);
            equippedPanel.SetActive(isActive);
            if (isActive)
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
            if (slot == null || slot.item == null)
                continue;

            GameObject slotGO = Instantiate(slotPrefab, slotsParent);
            // Slot’a item ikonunu ve ismini/adetini yerleştir
            Image iconImage = slotGO.transform.Find("Icon")?.GetComponent<Image>();
            Text labelText = slotGO.transform.Find("Label")?.GetComponent<Text>();

            if (iconImage != null && slot.item.icon != null)
                iconImage.sprite = slot.item.icon;
            if (labelText != null)
                labelText.text = slot.item.itemName + " x" + slot.count;
        }

        // Önce eski equipped slotlarını temizle
        foreach (Transform child in equippedSlotsParent)
            Destroy(child.gameObject);

        // Kuşanılan itemler için slot oluştur
        if (Inventory.Instance.equippedItems != null)
        {
            foreach (var equipped in Inventory.Instance.equippedItems)
            {
                if (equipped == null || equipped.item == null)
                    continue;

                GameObject equippedSlotGO = Instantiate(equippedSlotPrefab, equippedSlotsParent);
                Image iconImage = equippedSlotGO.transform.Find("Icon")?.GetComponent<Image>();
                Text labelText = equippedSlotGO.transform.Find("Label")?.GetComponent<Text>();

                if (iconImage != null && equipped.item.icon != null)
                    iconImage.sprite = equipped.item.icon;
                if (labelText != null)
                    labelText.text = equipped.item.itemName;
            }
        }
    }
}