using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    public InventorySO inventory;

    public ItemData pistolAmmo;
    public ItemData patch;
    public ItemData keycard;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            inventory.AddItem(pistolAmmo, 10);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            inventory.AddItem(patch, 1);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.AddItem(keycard, 1);
        }
    }
}
