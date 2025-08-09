using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventorySO Inventory;
    public GameObject ItemSlotPrefab;
    public Transform ContentPanel;

    void Start()
    {
        RefreshUI();
    }

    private Transform FindDeepChild(Transform parent, string Name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == Name)
                return child;
            var result = FindDeepChild(child, Name);
            if (result != null)
                return result;
        }
        return null;
    }

    public void RefreshUI()
    {
        foreach (Transform child in ContentPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (var entry in Inventory.items)
        {
            GameObject slot = Instantiate(ItemSlotPrefab, ContentPanel);

            var NameTransform = FindDeepChild(slot.transform, "Name");
            if (NameTransform != null)
            {
                var NameText = NameTransform.GetComponent<TMP_Text>();
                if (NameText != null)
                    NameText.text = entry.item.name;
            }

            var qtyTransform = FindDeepChild(slot.transform, "Quantity");
            if (qtyTransform != null)
            {
                var qtyText = qtyTransform.GetComponent<TMP_Text>();
                if (qtyText != null)
                    qtyText.text = entry.quantity.ToString();
            }

            var IconTransform = FindDeepChild(slot.transform, "Image");
            if (IconTransform != null)
            {
                var IconImage = IconTransform.GetComponent<Image>();
                if (IconImage != null)
                    IconImage.sprite = entry.item.icon;
            }
        }
    }

}
