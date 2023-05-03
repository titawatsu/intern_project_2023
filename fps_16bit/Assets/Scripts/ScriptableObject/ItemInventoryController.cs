using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryController : MonoBehaviour
{
    public Button RemoveButton;
    Item item;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.MedKit:
                Player.instance.IncreaseHealth(item.value);
                break;
            case Item.ItemType.Ammo:
                break;
            case Item.ItemType.Grenade:
                break;
        }
        RemoveItem();
    }
}
