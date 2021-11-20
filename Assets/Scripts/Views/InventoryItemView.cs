using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private GameObject allObjects;
    public Image ItemImage => itemImage;
    public TextMeshProUGUI ItemCount => itemCount;
    public GameObject AllObjects => allObjects;
}
