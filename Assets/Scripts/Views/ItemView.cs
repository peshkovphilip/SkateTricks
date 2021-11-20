using System;
using UnityEngine;

public class ItemView : MonoBehaviour
{
    public event Action<ItemView, Collider2D> OnEnter;
    [SerializeField] private EItemType itemType;
    [SerializeField] private bool isDestroyable = true;
    [SerializeField] private bool isPickable = false;
    [SerializeField] private bool isWearable = false;
    [SerializeField] private Sprite itemImage;

    public EItemType ItemType
    {
        get => itemType;
        set
        {
            itemType = value;
        }
    }
    public bool IsPickable => isPickable;
    public bool IsDestroyable => isDestroyable;
    public bool IsWearable => isWearable;
    public Sprite ItemImage => itemImage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(this, collision);
        //if (isDestroyable) Destroy(gameObject); // как перенести это в контроллер?
    }
}
