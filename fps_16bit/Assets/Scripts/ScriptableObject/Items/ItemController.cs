using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;

namespace fps_16bit
{
    public class ItemController : MonoBehaviour
    {
        public Item item;

        public ItemInventoryController itemInventoryController;

        private void OnDestroy()
        {
            itemInventoryController.UseItem(item);
        }
    }

}