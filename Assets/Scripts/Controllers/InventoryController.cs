using UnityEngine;

public class InventoryController : IStarter
{
    private InventoryModel inventory = new InventoryModel();
    private ItemView[] items;

    public InventoryModel Inventory => inventory;
    public void Starter()
    {
        Debug.Log("start InventoryController");
        items = Object.FindObjectsOfType<ItemView>();
        foreach (ItemView item in items)
        {
            item.OnEnter += PickUp;
        }
    }

    private void PickUp(ItemView itemView, Collider2D collider)
    {
        if (collider.GetComponent<PlayerBodyView>() != null)
        {
            if (itemView.IsPickable)
            {
                if (inventory.MaxSlots > inventory.Items.Count)
                {
                    ItemModel newItem = new ItemModel(1, itemView.ItemType);
                    inventory.AddItem(newItem);
                }
            }
            if (itemView.ItemType == ItemType.Pot)
            {
                if (inventory.RemoveItem(ItemType.Flower))
                {
                    itemView.ItemType = ItemType.FlowersPot;
                    itemView.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/items/flower-pot_50");
                }
            }
        }
        
    }

    
}
