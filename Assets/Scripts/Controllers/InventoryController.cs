using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : IStarter
{
    private InventoryModel inventory = new InventoryModel();
    private ItemView[] items;
    public InventoryModel Inventory => inventory;
    private UICanvasView uiCanvasView;
    private PlayerView playerView;
    private List<AbilityModel> abilities = new List<AbilityModel>();

    public InventoryController(UICanvasView uiCanvasView, PlayerView playerView, List<AbilityModel> abilities)
    {
        this.uiCanvasView = uiCanvasView;
        this.playerView = playerView;
        this.abilities = abilities;
    }
    public void Starter()
    {
        Debug.Log("start InventoryController");
        items = Object.FindObjectsOfType<ItemView>();
        foreach (ItemView item in items)
        {
            item.OnEnter += PickUp;
        }
        UpdateInventoryView();
    }

    private void PickUp(ItemView itemView, Collider2D collider)
    {
        if (collider.GetComponent<PlayerBodyView>() != null)
        {
            if (itemView.IsPickable)
            {
                if (inventory.MaxSlots > inventory.Items.Count)
                {
                    ItemModel itemModel = new ItemModel(1, itemView.ItemType, itemView.ItemImage);
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
            if (itemView.ItemType == EItemType.Pot)
            {
                if (inventory.RemoveItem(EItemType.Flower))
                {
                    itemView.ItemType = EItemType.FlowersPot;
                    itemView.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/items/flower-pot_50");
                }
            }

            UpdateInventoryView();
        }
        
    }

    private void UpdateInventoryView()
    {
        InventoryView inventoryView = uiCanvasView.Panels[(int) PanelType.Inventory].GetComponent<InventoryView>();
        for (int i = 0; i < inventoryView.InventoryItemViews.Length; i++)
        {
            InventoryItemView inventoryItemView = inventoryView.InventoryItemViews[i];
            if (inventory.Items.Count > i)
            {
                inventoryItemView.AllObjects.SetActive(true);
                inventoryItemView.ItemImage.sprite = inventory.Items[i].Image;  
                inventoryItemView.ItemCount.text = inventory.Items[i].Count.ToString();   
            }
            else
            {
                inventoryItemView.AllObjects.SetActive(false);
            }
        }
    }
    
}
