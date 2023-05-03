using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    public Transform itemContent;
    public GameObject inventoryItem;
    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }

    private void ListItem()
    {
        foreach (Transform item in itemContent)
        {
            Destroy(item.gameObject);
        }
        foreach (var item in items)
        {
            GameObject itemObject = Instantiate(inventoryItem, itemContent);

            var itemName = itemObject.transform.Find("ItemName").GetComponent<Text>();
            var itemIcon = itemObject.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    public void EnableItemsRemove()
    {
        if(EnableRemove)
    }
}
