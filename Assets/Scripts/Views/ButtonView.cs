using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonView : MonoBehaviour, IPointerClickHandler
{
    public event Action<Tap> OnTap;
    [SerializeField] private Tap tapType;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        OnTap?.Invoke(tapType);
    }
}
