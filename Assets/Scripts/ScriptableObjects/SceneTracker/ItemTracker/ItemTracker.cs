using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemTracker : ScriptableObject
{
    public List<string> ItemIDs = new List<string>();

    public bool IsCollected(string id)
    {
        return ItemIDs.Contains(id);
    }

    public void MarkAsCollected(string id)
    {
        if (!ItemIDs.Contains(id))
        {
            ItemIDs.Add(id);
        }
    }
}
