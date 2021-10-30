using System;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    public event Action<ItemView, Collider2D> OnEnter;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int coins = 0;
    [SerializeField] private bool isDestroyable = true;
    [SerializeField] private bool isPickable = false;

    public ItemType ItemType
    {
        get => itemType;
        set
        {
            itemType = value;
        }
    }
    public int Coins => coins;
    public bool IsPickable => isPickable;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(this, collision);
        if (isDestroyable) Destroy(gameObject); // как перенести это в контроллер?
    }
}
