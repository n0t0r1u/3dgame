using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<InventorySlot> items = new List<InventorySlot>();
    public int space = 20;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool Add(Item item)
    {
        if (items.Count >= space)
        {
            Debug.Log("Envanter dolu!");
            return false;
        }

        foreach (InventorySlot slot in items)
        {
            if (slot.item == item && item.isStackable && slot.count < item.maxStack)
            {
                slot.count++;
                return true;
            }
        }

        items.Add(new InventorySlot(item));
        return true;
    }

    public void Remove(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                items[i].count--;
                if (items[i].count <= 0)
                    items.RemoveAt(i);
                return;
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int count;

    public InventorySlot(Item item)
    {
        this.item = item;
        this.count = 1;
    }
}