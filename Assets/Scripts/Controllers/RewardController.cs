using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class RewardController : IStarter
{
    private ButtonsUIModel _buttonsUIModel;
    private TableRewardView tableRewardView;
    private List<RewardModel> rewards = new List<RewardModel>();
    private PlayerData _playerData;
    private InventoryModel _inventory;
    public event Action<PanelType> PanelView;
    
    public RewardController(PlayerData playerData, InventoryModel inventoryModel, ButtonsUIModel buttonsUIModel)
    {
        _buttonsUIModel = buttonsUIModel;
        _playerData = playerData;
        _inventory = inventoryModel;
    }
    
    public void Starter()
    {
        Debug.Log("start RewardController");
        tableRewardView = GameObject.FindObjectOfType<TableRewardView>(true);
        CreateRewards();
        DrawRewards();
        ActivateSubscriptions();
    }

    private void ActivateSubscriptions()
    {
        foreach (ButtonUIView button in _buttonsUIModel.Buttons)
        {
            button.OnTap += TryAction;
        }
    }
    

    public void TryAction(TapUI typeTap)
    {
        switch (typeTap)
        {
            case TapUI.GrabReward:
                GrabReward();
                break;    
        }
    }

    private void CreateRewards()
    {
        rewards.Clear();
        rewards.Add(new RewardModel(1, EItemType.Coin, 5));
        rewards.Add(new RewardModel(2, EItemType.Coin, 10));
        rewards.Add(new RewardModel(3, EItemType.Coin, 25));
        rewards.Add(new RewardModel(4, EItemType.JetPack, 1));
        rewards.Add(new RewardModel(5, EItemType.Coin, 50));
        rewards.Add(new RewardModel(6, EItemType.JetPack, 5));
    }

    private void DrawRewards()
    {
        foreach (var reward in rewards)
        {
            GameObject newItem = Object.Instantiate(tableRewardView.ItemReward, tableRewardView.transform.position, Quaternion.identity);
            newItem.transform.SetParent(tableRewardView.transform);
            ItemRewardView itemRewardView = newItem.GetComponent<ItemRewardView>();
            itemRewardView.RewardValue = reward.Count;
            itemRewardView.RewardDay = reward.Day;
            itemRewardView.RewardImage = reward.ItemType;
            if (_playerData.GrabRewards + 1 > reward.Day)
            {
                itemRewardView.RewardActive = false;
                itemRewardView.GrabActive = false;
            }
            else
            {
                if (_playerData.GrabRewards + 1 < reward.Day)
                {
                    itemRewardView.RewardActive = true;
                    itemRewardView.GrabActive = false;
                }
                else
                {
                    if (_playerData.GrabRewards > 0)
                    {
                        if (IsTodayBiggerThanRewardDay())
                        {
                            itemRewardView.RewardActive = true;
                            itemRewardView.GrabActive = true;
                            ButtonUIView buttonUIView = itemRewardView.RewardGrab.GetComponent<ButtonUIView>();
                            _buttonsUIModel.AddButton(buttonUIView);    
                        }
                        else
                        {
                            
                            PanelView?.Invoke(PanelType.Menu);
                            itemRewardView.RewardActive = true;
                            itemRewardView.GrabActive = false;
                        }
                    }
                    else
                    {
                        itemRewardView.RewardActive = true;
                        itemRewardView.GrabActive = true;
                        ButtonUIView buttonUIView = itemRewardView.RewardGrab.GetComponent<ButtonUIView>();
                        _buttonsUIModel.AddButton(buttonUIView);
                    }
                }
            }
        }      
    }
    
    public bool IsTodayBiggerThanRewardDay()
    {
        DateTime dateTime = new DateTime(_playerData.FirstGrabReward.Year, _playerData.FirstGrabReward.Month, _playerData.FirstGrabReward.Day);
        double seconds = new TimeSpan(dateTime.Ticks).TotalSeconds;
        double secondsNow = new TimeSpan(DateTime.UtcNow.Ticks).TotalSeconds;
        float daySeconds = _playerData.GrabRewards * 24 * 60 * 60;
        return (secondsNow > seconds + daySeconds);
    }

    private void GrabReward()
    {
        _inventory.PickUpItem(new ItemModel(rewards[_playerData.GrabRewards].Count, rewards[_playerData.GrabRewards].ItemType));
        if (_playerData.GrabRewards == 0)
        {
            _playerData.FirstGrabReward =
                new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
        }
        _playerData.GrabRewards++;
        Game.SaveGame(_playerData);
        
        PanelView?.Invoke(PanelType.Menu);
        GameAnalytics.SendMessage("grab_reward");
    }
    
    
}
