using System;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    public event Action<ItemView, Collider2D> OnEnter;
    [SerializeField] private EItemType itemType;
    [SerializeField] private bool isDestroyable = true;
    [SerializeField] private bool isPickable;
    [SerializeField] private bool isWearable;
    [SerializeField] private Sprite itemImage;
    [SerializeField] private SpriteRenderer itemSprite;

    public EItemType ItemType
    {
        get => itemType;
        set => itemType = value;
    }
    public bool IsPickable => isPickable;
    public bool IsDestroyable => isDestroyable;
    public bool IsWearable => isWearable;
    public Sprite ItemImage => itemImage;
    public SpriteRenderer ItemSprite => itemSprite;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(this, collision);
    }
}
