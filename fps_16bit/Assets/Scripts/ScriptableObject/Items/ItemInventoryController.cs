using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
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
                case Item.ItemType.GasCan:
                    Player.instance.IncreaseGasAmount(item.value);
                    break;
                case Item.ItemType.Battery:
                    break;
                case Item.ItemType.Flashlight:
                    Player.instance.getFlashlight = true;
                    break;
                case Item.ItemType.Book:
                    Player.instance.IncreaseBook(item.value);
                    break;
            }
        }
    }
}
