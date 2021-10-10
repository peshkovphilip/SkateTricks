using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ETypes.Tap> OnTap;
    [SerializeField] private ETypes.Tap tapType;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        OnTap?.Invoke(tapType);
    }
}
