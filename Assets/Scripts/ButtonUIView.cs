using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonUIView : MonoBehaviour, IPointerClickHandler
{
    public event Action<TapUI> OnTap;
    [SerializeField] private TapUI tapType;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("type : " + tapType + " Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        OnTap?.Invoke(tapType);
    }
}