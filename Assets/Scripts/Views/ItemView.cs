using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemView : MonoBehaviour
{
    public event Action<ItemView, Collider2D> OnEnter;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int coins = 0;
    [SerializeField] private bool isDestroyable = true;

    public ItemType ItemType => itemType;
    public int Coins => coins;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter?.Invoke(this, collision);
        if (isDestroyable) Destroy(gameObject); // как перенести это в контроллер?
    }
}
