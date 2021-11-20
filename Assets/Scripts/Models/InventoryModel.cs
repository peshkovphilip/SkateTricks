using System.Collections.Generic;
using System;
using UnityEngine;

public class InventoryModel 
{
    private int _maxSlots = 4;
    private List<ItemModel> items = new List<ItemModel>();

    public event Action<ItemModel> RemoveAction;
    public event Action<ItemModel> AddAction;
    public int MaxSlots => _maxSlots;
    public List<ItemModel> Items => items;


    public void AddItem(ItemModel item)
    {
        items.Add(item);
        AddAction?.Invoke(item);
    }
    public bool RemoveItem(EItemType itemType)
    {
        if (items.FindAll(x => x.ItemType == itemType).Count > 0)
        {
            ItemModel item = items.Find(x => x.ItemType == itemType);
            if (item.Count > 0)
            {
                if (item.Count > 1)
                {
                    item.Count--;
                }
                else
                {
                    items.Remove(item);
                }
                RemoveAction?.Invoke(item);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void PickUpItem(ItemModel itemModel)
    {
        if (items.FindAll(x => x.ItemType == itemModel.ItemType).Count == 0)
        {
            AddItem(itemModel);
        }
        else
        {
            items.Find(x => x.ItemType == itemModel.ItemType).Count += itemModel.Count;
        }
    }
}
