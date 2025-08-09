using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemLogic : MonoBehaviour
{
    public string ID;
    public ItemTracker Tracker;

    private GameObject Parent;
    public ItemData item;
    public int quantity = 1;
    public InventorySO Inventory;

    public void Start()
    {
        Parent = transform.parent.gameObject;
        ID = Parent.name;

        if (Tracker.IsCollected(ID))
        {
            Parent.SetActive(false);
        }
    }

    public void OnClick()
    {
        Inventory.AddItem(item, quantity);
        Tracker.MarkAsCollected(ID);
        Parent.SetActive(false);
    }
}
