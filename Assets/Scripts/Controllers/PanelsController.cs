using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class PanelsController : IStarter
{
    private List<PanelView> panels = new List<PanelView>();
    private PlayerModel playerModel;
    private UICanvasView _uiCanvasView;
    private List<ButtonUIView> _buttons;
    private RewardController _rewardController;
    private ItemView[] _itemViews;
    private PlayerView _playerView;
    private UIController _uiController;
    
    public PanelsController(UICanvasView uiCanvasView, RewardController rewardController, ItemView[] itemViews, PlayerView playerView, UIController uiController)
    {
        _uiCanvasView = uiCanvasView;
        _rewardController = rewardController;
        _itemViews = itemViews;
        _playerView = playerView;
        _uiController = uiController;
    }
    
    public void Starter()
    {
        Debug.Log("start PanelsController");
        playerModel = Object.FindObjectOfType<PlayerModel>(true);
        panels = _uiCanvasView.Panels.ToList();
        UnActiveAllPanels();

        if (Game.RestartGame)
        {
            ViewPanel(PanelType.LevelGame);
        }
        else
        {
            if (_rewardController.IsTodayBiggerThanRewardDay())
            {
                ViewPanel(PanelType.DailyReward);
            }
            else
            {
                ViewPanel(PanelType.Menu);
            }    
        }
        
        
        playerModel.PanelView += ViewPanel;
        _rewardController.PanelView += ViewPanel;
        _uiController.PanelView += ViewPanel;
        
        foreach (ItemView item in _itemViews)
        {
            item.OnEnter += GetItem;
        }
    }
    

    private void UnActiveAllPanels()
    {
        foreach (PanelView panel in panels)
        {
            panel.gameObject.SetActive(false);
        }
    }
    
    
    public void ViewPanel(PanelType panelType)
    {
        Debug.Log("view panel = " + panelType);
        UnActiveAllPanels();
        panels.Find(x => x.PanelType == panelType).gameObject.SetActive(true);

        switch (panelType)
        {
            case PanelType.LevelGame:
                panels.Find(x => x.PanelType == PanelType.Quests).gameObject.SetActive(true);
                panels.Find(x => x.PanelType == PanelType.Inventory).gameObject.SetActive(true);
                break;
            case PanelType.LevelDone:
                GameAnalytics.SendMessage("level_done");
                break;
            case PanelType.LevelLose:
                GameAnalytics.SendMessage("level_lose");
                break;
        }
    }
    
    public void GetItem(ItemView itemView, Collider2D collider)
    {
        if (collider == _playerView.collider)
        {
            if (itemView.ItemType == EItemType.Finish)
            {
                ViewPanel(PanelType.LevelDone);
            }
        }
    }

    public List<PanelView> GetPanels()
    {
        return panels;
    }
}
