using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventoryController : MonoBehaviour
{
    public Buttom RemoveButton;
    
    Item item;

    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);
    }
    public void AddItem(Item newItem)
    
    
}
