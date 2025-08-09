using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    public const int MaxSlots = 8;
    public List<InventorySlot> items = new List<InventorySlot>(MaxSlots);

    public bool AddItem(ItemData newItem, int amount = 1)
    {
        if (newItem.isStackable)
        {
            foreach (var slot in items)
            {
                if (slot.item == newItem && slot.quantity < newItem.maxStack)
                {
                    int spaceLeft = newItem.maxStack - slot.quantity;
                    int amountToAdd = Mathf.Min(spaceLeft, amount);

                    slot.quantity += amountToAdd;
                    amount -= amountToAdd;

                    if (amount <= 0) return true;
                }
            }
        }

        while (amount > 0)
        {
            if (items.Count >= MaxSlots)
            {
                Debug.LogWarning("Inventory full");
                return false;
            }

            int amountToAdd = newItem.isStackable ? Mathf.Min(newItem.maxStack, amount) : 1;
            items.Add(new InventorySlot(newItem, amountToAdd));
            amount -= amountToAdd;
        }

        return true;
    }
    public bool RemoveItem(ItemData itemToRemove, int amount = 1) {
        for (int i = items.Count - 1; i >= 0 && amount > 0; i--) {
            var slot = items[i];
            if (slot.item == itemToRemove)
            {
                if (slot.quantity > amount)
                {
                    slot.quantity -= amount;
                    return true;
                }
                else
                {
                    amount -= slot.quantity;
                    items.RemoveAt(i);
                }
            }
        }

        return amount <= 0;
    }
}
