using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonsUIModel
{
    private List<ButtonUIView> buttons;
    public event Action<ButtonUIView> AddButtonAction;
    
    public void AddButton(ButtonUIView buttonUIView)
    {
        buttons.Add(buttonUIView);
        AddButtonAction?.Invoke(buttonUIView);
    }
    public List<ButtonUIView> Buttons
    {
        get => buttons;
        set => buttons = value;
    }
}
