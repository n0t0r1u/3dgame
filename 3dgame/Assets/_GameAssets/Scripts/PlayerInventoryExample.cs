using UnityEngine;

public class PlayerInventoryExample : MonoBehaviour
{
    public Item testItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Instance.Add(testItem);
            Debug.Log("Item eklendi: " + testItem.itemName);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Inventory.Instance.Remove(testItem);
            Debug.Log("Item çıkarıldı: " + testItem.itemName);
        }
    }
}