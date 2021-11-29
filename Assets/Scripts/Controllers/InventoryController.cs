using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class InventoryController : IStarter
{
    private InventoryModel inventory = new InventoryModel();
    private ItemView[] _items;
    public InventoryModel Inventory => inventory;
    private UICanvasView uiCanvasView;
    private PlayerView playerView;
    private List<AbilityModel> abilities = new List<AbilityModel>();

    public InventoryController(UICanvasView uiCanvasView, PlayerView playerView, List<AbilityModel> abilities, InventoryModelOnly inventoryModelOnly, ItemView[] items)
    {
        this.uiCanvasView = uiCanvasView;
        this.playerView = playerView;
        this.abilities = abilities;
        this.inventory.Inventory = inventoryModelOnly;
        _items = items;

    }
    public void Starter()
    {
        Debug.Log("start InventoryController");
        //items = Object.FindObjectsOfType<ItemView>(true);
        foreach (ItemView item in _items)
        {
            item.OnEnter += PickUp;
        }

        inventory.PickUpAction += UpdateInventoryView;
        UpdateInventoryView();
    }

    private void PickUp(ItemView itemView, Collider2D collider)
    {
        if (collider.GetComponent<PlayerBodyView>() == null)
            return;
        
        if (itemView.IsPickable)
        {
            if (inventory.Inventory.MaxSlots > inventory.Inventory.Items.Count)
            {
                ItemModel itemModel = new ItemModel(1, itemView.ItemType);
                inventory.PickUpItem(itemModel);
            }
            if (itemView.IsWearable)
            {
                if (itemView.ItemType == EItemType.JetPack)
                {
                    playerView.Body.JetPackSprite.sprite = itemView.ItemImage;
                    abilities.Find(x => x.AbilityType == EAbilityType.Jump).Active = true; // перенести в айтим
                }
            }
            if (itemView.IsDestroyable)
            {
                GameObject.Destroy(itemView.gameObject);
            }
        }
        
        if ((itemView.ItemType == EItemType.Pot) && (inventory.RemoveItem(EItemType.Flower)))
        {
            itemView.ItemType = EItemType.FlowersPot;
            itemView.ItemSprite.sprite = Resources.Load<Sprite>("sprites/items/flower-pot_50");
        }

        UpdateInventoryView();
        
    }

    private void UpdateInventoryView(ItemModel itemModel)
    {
        UpdateInventoryView();
    }
    private void UpdateInventoryView()
    {
        InventoryView inventoryView = uiCanvasView.Panels[(int) PanelType.Inventory].GetComponent<InventoryView>();
        for (int i = 0; i < inventoryView.InventoryItemViews.Length; i++)
        {
            InventoryItemView inventoryItemView = inventoryView.InventoryItemViews[i];
            if (inventory.Inventory.Items.Count > i)
            {
                inventoryItemView.AllObjects.SetActive(true);
                inventoryItemView.ItemImage.sprite = Resources.Load<Sprite>("sprites/items/"+inventory.Inventory.Items[i].Image);  
                inventoryItemView.ItemCount.text = inventory.Inventory.Items[i].Count.ToString();   
            }
            else
            {
                inventoryItemView.AllObjects.SetActive(false);
            }
        }
    }
    
}
