using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public ItemInventoryController[] inventoryControllers;
    
    public List<Item> items = new List<Item>();

    public Toggle enableRemove;

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
            var removeButton = itemObject.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
            
            if (enableRemove.isOn)
                removeButton.gameObject.SetActive(true);
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (enableRemove.isOn)
        {
            foreach (Transform item in itemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in itemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        inventoryControllers = itemContent.GetComponentsInChildren<ItemInventoryController>();

        for (int i = 0; i < items.Count; i++)
        {
            inventoryControllers[i].AddItem(items[i]);
        }
    }
}
