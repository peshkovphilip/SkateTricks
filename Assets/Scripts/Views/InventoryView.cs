using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour
{
    [SerializeField] private InventoryItemView[] inventoryItemView;
    public InventoryItemView[] InventoryItemViews => inventoryItemView;
}
