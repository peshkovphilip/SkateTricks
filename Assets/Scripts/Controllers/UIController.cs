using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : IStarter
{
    private List<PanelView> _panels;
    private ButtonUIView[] buttons;
    
    public UIController(List<PanelView> panels)
    {
        _panels = panels;
    }
    
    public void Starter()
    {
        Debug.Log("start UIController");
        
        buttons = Object.FindObjectsOfType<ButtonUIView>(true); //как это передать в конструктор без поиска и не присваивая все объекты в инспекторе?
        foreach (ButtonUIView button in buttons)
        {
            button.OnTap += TryAction;
        }
    }

    public void TryAction(TapUI typeTap)
    {
        switch (typeTap)
        {
            case TapUI.StartGame:
                StartGame();
                break;
            default:
                break;               
        }
    }

    private void StartGame()
    {
        Debug.Log(_panels.Count);
        _panels.Find(x => x.PanelType == PanelType.Menu).gameObject.SetActive(false);
        _panels.Find(x => x.PanelType == PanelType.LevelGame).gameObject.SetActive(true);
        _panels.Find(x => x.PanelType == PanelType.Quests).gameObject.SetActive(true);
        _panels.Find(x => x.PanelType == PanelType.Inventory).gameObject.SetActive(true);
        GameAnalytics.SendMessage("start_game");
    }
}
