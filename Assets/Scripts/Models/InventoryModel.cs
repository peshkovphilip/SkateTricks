using System.Collections.Generic;
using System;


public class InventoryModel
{

    private InventoryModelOnly inventory;
    public event Action<ItemModel> RemoveAction;
    public event Action<ItemModel> AddAction;
    public event Action<ItemModel> PickUpAction;


    public InventoryModelOnly Inventory
    {
        get => inventory;
        set => inventory = value;
    }

    public void AddItem(ItemModel item)
    {
        inventory.Items.Add(item);
        AddAction?.Invoke(item);
    }
    public bool RemoveItem(EItemType itemType)
    {
        if (inventory.Items.FindAll(x => x.ItemType == itemType).Count > 0)
        {
            ItemModel item = inventory.Items.Find(x => x.ItemType == itemType);
            if (item.Count > 0)
            {
                if (item.Count > 1)
                {
                    item.Count--;
                }
                else
                {
                    inventory.Items.Remove(item);
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
        if (inventory.Items.FindAll(x => x.ItemType == itemModel.ItemType).Count == 0)
        {
            AddItem(itemModel);
        }
        else
        {
            inventory.Items.Find(x => x.ItemType == itemModel.ItemType).Count += itemModel.Count;
        }
        PickUpAction?.Invoke(itemModel);
    }
}
