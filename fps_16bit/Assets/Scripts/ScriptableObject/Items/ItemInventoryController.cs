using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventoryController : MonoBehaviour
{
    Item item;

    public void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.MedKit:
                Debug.Log("Increase Health by" + item.value);
                Player.instance.IncreaseHealth(item.value);
                break;
            case Item.ItemType.Ammo:
                break;
            case Item.ItemType.Grenade:
                break;
        }
    }
}