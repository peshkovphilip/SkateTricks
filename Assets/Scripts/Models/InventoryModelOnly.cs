using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryModelOnly
{
    private int _maxSlots = 4;
    private List<ItemModel> items = new List<ItemModel>();
    
    public int MaxSlots
    {
        get => _maxSlots;
        set => _maxSlots = value;
    }

    public List<ItemModel> Items
    {
        get => items;
        set => items = value;
    }
}
